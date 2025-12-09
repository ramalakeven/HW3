from functools import wraps


def count_calls(func):
    def wrapper(*args, **kwargs):
        wrapper.calls += 1
        return func(*args, **kwargs)
    wrapper.calls = 0
    return wrapper

@count_calls
def greet(name):
    return f"Hello {name}!"

if __name__ == "__main__":
    print(greet("Danila"))
    print(greet("World"))
    print(greet.calls)