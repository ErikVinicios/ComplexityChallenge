// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text;

int seed = DateTime.Now.Millisecond;
Random rand = new Random(seed);

MostrarTempo(1000, 20);

int solucao1(int[,] mat, int N)
{
    int maior = int.MinValue;

    foreach (int num in mat)
    {
        if (num > maior)
        {
            maior = num;
        }
    }

    return maior;
}

int solucao2(int[,] mat, int N)
{
    int maior = int.MinValue;

    for (int i=0;i<N-1;i++){
        for (int j=0;j<N-1;j++)
        {
            if (maior < mat[i,j])
            {
                maior = mat[i,j];

                if (i == 0 && j == 0)
                {
                    if (maior < mat[i+1,j+1])
                    {
                        if (maior < mat[i,j+1] && maior < mat[i+1,j])
                        {
                            return maior;
                        }
                    }
                }
                else if (i == 0 && j == N-1)
                {
                    if (maior < mat[i+1,j-1])
                    {
                        if (maior < mat[i,j-1] && maior < mat[i+1,j])
                        {
                            return maior;
                        }
                    }
                }
                else if (i == 0)
                {
                    if (maior < mat[i+1,j-1] && maior < mat[i+1,j+1])
                    {
                        if (maior < mat[i,j-1] && maior < mat[i+1,j] && maior < mat[i,j+1])
                        {
                            return maior;
                        }
                    }
                }
                else if (i == N-1 && j == 0)
                {
                    if (maior < mat[N-2,j+1])
                    {
                        if (maior < mat[N-2,j] && maior < mat[i,j+1])
                        {
                            return maior;
                        }
                    }
                }
                else if (i == N-1 && j == N-1)
                {
                    if (maior < mat[N-2,j-1])
                    {
                        if (maior < mat[N-2,j] && maior < mat[i,j-1])
                        {
                            return maior;
                        }
                    }
                }
                else if (i == N-1)
                {
                    if (maior < mat[i-1,j-1] && maior < mat[i-1,j+1])
                    {
                        if (maior < mat[i,j-1] && maior < mat[i-1,j] && maior < mat[i,j+1])
                        {
                            return maior;
                        }
                    }
                }
                else if (j == 0)
                {
                    if (maior < mat[i-1,j+1] && maior < mat[i+1,j+1])
                    {
                        if (maior < mat[i-1,j] && maior < mat[i,j+1] && maior < mat[i+1,j])
                        {
                            return maior;
                        }
                    }
                }
                else if (j == N-1)
                {
                    if (maior < mat[i-1,j-1] && maior < mat[i+1,j-1])
                    {
                        if (maior < mat[i-1,j] && maior < mat[i,j-1] && maior < mat[i+1,j])
                        {
                            return maior;
                        }
                    }
                }
                else if (maior < mat[i-1,j-1] && maior < mat[i-1,j+1] && maior < mat[i+1,j-1] && maior < mat[i+1,j+1])
                {
                    if (maior < mat[i-1,j] && maior < mat[i,j-1] && maior < mat[i+1,j] && maior < mat[i,j+1])
                    {
                        return maior;
                    }
                }
            }
        }
    }

    return maior;
}

int solucao3(int[,] mat, int N)
{
    int i = (N-1) / 2;
    int j = (N-1) / 2;
    int maior = mat[i,j];

    while (true)
    {
        if (i != 0 && j != 0)
        {
            if (maior < mat[i-1,j-1])
            {
                i -= 1;
                j -= 1;
                maior = mat[i,j];
                continue;
            }
        }
        if (i != 0 && j != N-1)
        {
            if (maior < mat[i-1,j+1])
            {
                i -= 1;
                j += 1;
                maior = mat[i,j];
                continue;
            }
        }
        if (i != N-1 && j != 0)
        {
            if (maior < mat[i+1,j-1])
            {
                i += 1;
                j -= 1;
                maior = mat[i,j];
                continue;
            }
        }
        if (i != N-1 && j != N-1)
        {
            if (maior < mat[i+1,j+1])
            {
                i += 1;
                j += 1;
                maior = mat[i,j];
                continue;
            }
        }
        break;
    }
    if (i != 0)
    {
        if (maior < mat[i-1,j])
        {
            i -= 1;
            maior = mat[i,j];
            return maior;
        }
    }
    if (j != 0)
    {
        if (maior < mat[i,j-1])
        {
            j -= 1;
            maior = mat[i,j];
            return maior;
        }
    }
    if (i != N-1)
    {
        if (maior < mat[i+1,j])
        {
            i += 1;
            maior = mat[i,j];
            return maior;
        }
    }
    if (j != N-1)
    {
        if (maior < mat[i,j+1])
        {
            j += 1;
            maior = mat[i,j];
            return maior;
        }
    }

    return maior;
}

int[,] GerarMatrizCebola(int N)
{
    int[,] mat = new int[N, N];
    int x = rand.Next(N),
        y = rand.Next(N),
        value = rand.Next(500, 1000),
        _x = 0,
        _y = 0;
    mat[x, y] = value;

    int delta = 1;
    int lastMinValue = value;
    int newMinValue = value;
    while (delta < N)
    {
        for (int i = -delta; i <= delta; i++)
        {
            var newValue = lastMinValue - rand.Next(1, 6);
            if (newValue < newMinValue)
                newMinValue = newValue;
            
            _x = x + i;
            _y = y - delta;
            if (_x < 0 || _x >= N || _y < 0 || _y >= N)
                continue;
            
            mat[_x, _y] = newValue;
        }
        
        for (int i = -delta; i <= delta; i++)
        {
            var newValue = lastMinValue - rand.Next(1, 6);
            if (newValue < newMinValue)
                newMinValue = newValue;
            
            _x = x + i;
            _y = y + delta;
            if (_x < 0 || _x >= N || _y < 0 || _y >= N)
                continue;
            
            mat[_x, _y] = newValue;
        }
        
        for (int j = -delta + 1; j < delta; j++)
        {
            var newValue = lastMinValue - rand.Next(1, 6);
            if (newValue < newMinValue)
                newMinValue = newValue;
            
            _x = x - delta;
            _y = y + j;
            if (_x < 0 || _x >= N || _y < 0 || _y >= N)
                continue;
            
            mat[_x, _y] = newValue;
        }
        
        for (int j = -delta + 1; j < delta; j++)
        {
            var newValue = lastMinValue - rand.Next(1, 6);
            if (newValue < newMinValue)
                newMinValue = newValue;
            
            _x = x + delta;
            _y = y + j;
            if (_x < 0 || _x >= N || _y < 0 || _y >= N)
                continue;
            
            mat[_x, _y] = newValue;
        }
        delta++;
        lastMinValue = newMinValue;
    }

    return mat;
}

void MostrarMatrizCebola(int[,] mat)
{
    int N = (int)Math.Sqrt(mat.LongLength);
    StringBuilder sb = new StringBuilder();
    for (int j = 0; j < N; j++)
    {
        if (j == 0)
        {
            for (int i = 0; i < N; i++)
            {
                if (i == 0)
                    sb.Append("┌");
                else sb.Append("┬");
                sb.Append("───────");
            }
            sb.Append("┐\n");
        }
        else
        {
            for (int i = 0; i < N; i++)
            {
                if (i == 0)
                    sb.Append("├");
                else sb.Append("┼");
                sb.Append("───────");
            }
            sb.Append("┤\n");
        }

        for (int k = 0; k < 2; k++)
        {
            for (int i = 0; i < N; i++)
            {
                sb.Append("│");
                sb.Append("       ");
            }
            sb.Append("|\n");
        }
        

        for (int i = 0; i < N; i++)
        {
            sb.Append("│");
            sb.Append(mat[i, j].ToString("  000  "));
        }
        sb.Append("|\n");

        for (int k = 0; k < 2; k++)
        {
            for (int i = 0; i < N; i++)
            {
                sb.Append("│");
                sb.Append("       ");
            }
            sb.Append("|\n");
        }
    }
        
    for (int i = 0; i < N; i++)
    {
        if (i == 0)
            sb.Append("└");
        else sb.Append("┴");
        sb.Append("───────");
    }
    sb.Append("┘\n");
    Console.WriteLine(sb.ToString());
}

void MostrarTempo(int N, int K)
{
    List<int[,]> list = new List<int[,]>();
    Console.Write($"Gerando {K} matrizes para testes: ");
    for (int k = 0; k < K; k++)
    {
        Console.Write($"{k + 1}.. ");
        list.Add(GerarMatrizCebola(N));
    }
    Console.WriteLine("\n");
    
    Stopwatch sw = new Stopwatch();

    Console.WriteLine("Testando solucao1...");
    sw.Start();
    foreach (var mat in list)
    {
        solucao1(mat, N);
    }
    sw.Stop();
    Console.WriteLine($"Solução 1 para n = {N}: {(double)sw.ElapsedMilliseconds / (double)K} ms / execução\n");
    sw.Reset();

    Console.WriteLine("Testando solucao2...");
    sw.Start();
    foreach (var mat in list)
    {
        solucao2(mat, N);
    }
    sw.Stop();
    Console.WriteLine($"Solução 2 para n = {N}: {(double)sw.ElapsedMilliseconds / (double)K} ms / execução\n");
    sw.Reset();

    Console.WriteLine("Testando solucao3...");
    sw.Start();
    foreach (var mat in list)
    {
        solucao3(mat, N);
    }
    sw.Stop();
    Console.WriteLine($"Solução 3 para n = {N}: {(double)sw.ElapsedMilliseconds / (double)K} ms / execução\n");
    sw.Reset();
}