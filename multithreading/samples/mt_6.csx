// Abaixo, uma solução real para este caso específico.
// Sempre que se considera a utilização do paralelismo,
// o fluxo lógico deve ser bem estudado para se ter
// certeza que existe um ganho real, caso contrário
// o único ganho será de problemas.

long total = 0;
int ciclos = 1000000000;

var lockObj = new object();

void Contar() {
    int i = 0;
    for ( i = 0; i < ciclos; i++);    
    Console.WriteLine($"Contei e deu {i}");

    lock (lockObj) {
        total += i;
    }
}

await Task.WhenAll(new Task[] {
    Task.Run(Contar),
    Task.Run(Contar),
    Task.Run(Contar),
    Task.Run(Contar)
});

Console.WriteLine($"O total foi {total}");
