import time

def timeit(label=""):
    def decorator(func):
        def wrapper(*args, **kwargs):
            start = time.perf_counter()
            result = func(*args, **kwargs)
            end = time.perf_counter()
            elapsed_ms = (end - start) * 1000
            name = label if label else func.__name__
            print(f"TIME: {name} took {elapsed_ms:.2f} ms")
            return result
        return wrapper
    return decorator

@timeit(label="heavy_sum")
def work():
    return sum(range(50_000))

if __name__ == "__main__":
    print(work())
