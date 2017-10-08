# SafeSynchronous
Helper to safely run a Task from a codebase not async.

## Why?

Sometimes you have to consume an async library from legacy codebase where going *async all the way* is not possible.

A common but generally **incorrect and dangerous** way to do it is usually:
```csharp
AsyncMethod().Wait();
// or
var result = AsyncMethodWithResult.Result;
```

If your application has a `SynchronizationContext` and the called method restore it (which should usually not happen in a library, but it's not always the case) your application will deadlock.

The common pattern to avoid the synchornization on the `SynchronizationContext` is to wrap the method in a Task:
```csharp
Task.Run(() => AsyncMethod());
// or
var result = Task.Run(() => AsyncMethodWithResult());
```

This technique is inefficient.

A better solution would be to temporary disable the SynchronizationContext, but it's not particulary intituitive. `SafeSynchronous` helps you!

This helper allow you to easily and **safely** execute a Task with a similar API as `Task.Run()` but without wrapping the call in a task.
It also make sure that any exception is not wrapped in a `AggregateException`.

## Usage

```csharp
SafeSynchronousTask.Run(() => AsyncMethod());
// or
var result = SafeSynchronousTask.Run(() => AsyncMethodWithResult());
// or
try {
    SafeSynchronousTask.Run(() => AsyncMethod());
} catch (CustomException ex) {
    // no AggregateException to unwrap :)
}
```
