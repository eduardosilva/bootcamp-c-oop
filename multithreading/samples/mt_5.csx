// É possível adquirir um lock para impedir que
// outras threads acessem a mesma memória que
// ao mesmo tempo.

long total = 0;
int ciclos = 1000000000;

var lockObj = new object();

void Contar() {
    int i = 0;
    for ( i = 0; i < ciclos; i++) {
        // A instrução abaixo cria uma trava,
        // permitindo somente uma thread, no entanto,
        // perdemos toda a vantagem do multithread
        lock (lockObj) {
            total += 1;
        }
    };
    Console.WriteLine($"Contei e deu {i}");
}

await Task.WhenAll(new Task[] {
    Task.Run(Contar),
    Task.Run(Contar),
    Task.Run(Contar),
    Task.Run(Contar)
});

Console.WriteLine($"O total foi {total}");
