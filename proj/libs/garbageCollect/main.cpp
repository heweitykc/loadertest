#include <stdlib.h>
#include <stdio.h>

#define STACK_MAX 256
#define INITAL_GC_THRESHOLD 8

typedef enum{
	OBJ_INT,
	OBJ_PAR
} ObjectType ;

typedef struct sObject{
	/* the next object in the list of all objects. */
	struct sObject* next;
	unsigned char marked; //mark bit
	ObjectType type;
	union 
	{
		int value;

		struct
		{
			struct sObject* head;
			struct sObject* tail;
		};
	};
} Object;

typedef struct{
	int numObjects;
	int maxObjects;
	
	Object* firstObject;
	Object* stack[STACK_MAX];
	int stackSize;
} VM;

void gc(VM* vm);

void assert(int condition, const char* message)
{
	if(!condition)
	{
		printf("%s\n",message);
		exit(1);
	}
}

VM* newVM(){
	VM* vm = (VM*)malloc(sizeof(VM));
	vm->stackSize = 0;
	vm->numObjects = 0;
	vm->maxObjects = INITAL_GC_THRESHOLD;
	return vm;
}

void freeVM(VM *vm)
{
	vm->stackSize = 0;
	gc(vm);
	free(vm);
}

void mark(Object* object)
{
	if (object->marked) return;
	object->marked = 1;
	if (object->type == OBJ_PAR) {
		mark(object->head);
		mark(object->tail);
	}
}

void markAll(VM* vm)
{
	for (int i = 0; i < vm->stackSize; i++) {
		mark(vm->stack[i]);
	}
}

void push(VM* vm, Object* value)
{
	assert(vm->stackSize < STACK_MAX, "statck overflow!");
	vm->stack[vm->stackSize++] = value;
}

Object* pop(VM* vm)
{
	assert(vm->stackSize>0, "statck overflow");
	return vm->stack[--vm->stackSize];
}

Object* newObject(VM* vm, ObjectType type)
{
	if(vm->numObjects == vm->maxObjects) gc(vm);
	
	Object* object = (Object*)malloc(sizeof(Object));
	object->type = type;
	object->next = vm->firstObject;
	vm->firstObject = object;
	object->marked = 0;

	vm->numObjects++;
	
	return object;
}

void sweep(VM* vm)
{
	Object** object = &vm->firstObject;
	while(*object){
		if(!(*object)->marked){
			Object* unreached = *object;
			*object = unreached->next;
			free(unreached);
		} else {
			(*object)->marked = 0;
			object = &(*object)->next;
		}
	}
}

void pushInt(VM* vm, int intValue)
{
	Object* object = newObject(vm,OBJ_INT);
	object->value = intValue;
	push(vm,object);
}

Object* pushPair(VM*vm)
{
	Object* object = newObject(vm, OBJ_PAR);
	object->tail = pop(vm);
	object->head = pop(vm);

	push(vm, object);
	return object;
}

void gc(VM* vm)
{
	markAll(vm);
	sweep(vm);
}

void test1()
{
	printf("Test 1: Objects on stack are perserved.\n");
	VM* vm = newVM();
	pushInt(vm,1);
	pushInt(vm,2);
	
	gc(vm);
	assert(vm->numObjects == 2, "Should have perserved objects.");
	freeVM(vm);
}

void test2()
{
	printf("Test 2: Unreached objects are collected.\n");
	VM* vm = newVM();
	pushInt(vm,1);
	pushInt(vm,2);
	pop(vm);
	pop(vm);
	
	gc(vm);
	assert(vm->numObjects == 0,"Should have collected objects.");
	freeVM(vm);
}

void test3()
{
	printf("Test 3: Reach nested objects .\n");
	VM* vm = newVM();
	pushInt(vm, 1);
	pushInt(vm, 2);
	pushPair(vm);
	pushInt(vm, 3);
	pushInt(vm, 4);
	pushPair(vm);
	pushPair(vm);
	pushPair(vm);
	
	gc(vm);
	assert(vm->numObjects == 7, "Should have reached objects.");
	freeVM(vm);
}

int main()
{
	test1();
	//test2();
	//test3();
	return 0;
}