#include <stdlib.h>
#include <assert.h>

typedef enum{
	OBJ_INT,
	OBJ_PAR
} ObjectType ;

typedef struct sObject{
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

#define STACK_MAX 256

typedef struct{
	Object* stack[STACK_MAX];
	int stackSize;
} VM;

VM* newVM(){
	VM* vm = (VM*)malloc(sizeof(VM));
	vm->stackSize = 0;
	return vm;
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
	Object* object = (Object*)malloc(sizeof(Object));
	object->type = type;
	return object;
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

int main()
{

	return 0;
}