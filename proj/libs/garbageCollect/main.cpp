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
	object->marked = 0;
	
	object->next = vm->firstObject;
	vm->firstObject = object;
	
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

int main()
{

	return 0;
}