``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19041.1348 (2004/May2020Update/20H1)
Intel Core i5-7400 CPU 3.00GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=5.0.402
  [Host]     : .NET 5.0.11 (5.0.1121.47308), X64 RyuJIT
  Job-NYRLRC : .NET 5.0.11 (5.0.1121.47308), X64 RyuJIT

RunStrategy=Throughput  

```
|       Method |       N |         Mean |      Error |     StdDev | Rank |       Gen 0 |       Allocated |
|------------- |-------- |-------------:|-----------:|-----------:|-----:|------------:|----------------:|
|   SerialTest |   32768 |     8.188 ms |  0.0264 ms |  0.0234 ms |    1 |           - |               - |
| ParallelTest |   32768 |     9.762 ms |  0.0449 ms |  0.0420 ms |    2 |   5578.1250 |    17,465,817 B |
| ParallelTest | 2097152 |   871.609 ms | 17.3778 ms | 21.3415 ms |    3 | 361000.0000 | 1,130,013,168 B |
|   SerialTest | 2097152 | 1,024.602 ms |  1.6776 ms |  1.5692 ms |    4 |           - |               - |
| ParallelTest | 4194304 | 1,784.710 ms | 33.4369 ms | 34.3372 ms |    5 | 724000.0000 | 2,264,145,248 B |
|   SerialTest | 4194304 | 2,212.331 ms |  5.5265 ms |  5.1695 ms |    6 |           - |               - |
