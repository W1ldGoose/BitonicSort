``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19041.1348 (2004/May2020Update/20H1)
Intel Core i5-7400 CPU 3.00GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=5.0.402
  [Host]     : .NET 5.0.11 (5.0.1121.47308), X64 RyuJIT
  Job-VPYSSH : .NET 5.0.11 (5.0.1121.47308), X64 RyuJIT

RunStrategy=Throughput  

```
|       Method |       N |       Mean |     Error |    StdDev | Rank |   Gen 0 | Allocated |
|------------- |-------- |-----------:|----------:|----------:|-----:|--------:|----------:|
| ParallelTest |   32768 |   4.447 ms | 0.0405 ms | 0.0359 ms |    1 | 39.0625 |  16,712 B |
|   SerialTest |   32768 |  11.362 ms | 0.0544 ms | 0.0482 ms |    2 |       - |         - |
| ParallelTest |  131072 |  20.913 ms | 0.6886 ms | 1.9869 ms |    3 | 31.2500 |  16,712 B |
|   SerialTest |  131072 |  56.949 ms | 0.9715 ms | 0.9088 ms |    4 |       - |      62 B |
| ParallelTest | 1048576 | 187.966 ms | 3.5322 ms | 3.6273 ms |    5 |       - |  16,995 B |
|   SerialTest | 1048576 | 598.313 ms | 6.6358 ms | 6.2071 ms |    6 |       - |   1,336 B |
