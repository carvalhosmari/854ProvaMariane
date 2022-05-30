//Conversores de posicionamento
Dictionary<String, int> linhasIndice = new Dictionary<String, int>();
linhasIndice.Add("A", 0);
linhasIndice.Add("B", 1);
linhasIndice.Add("C", 2);
linhasIndice.Add("D", 3);
linhasIndice.Add("E", 4);
linhasIndice.Add("F", 5);
linhasIndice.Add("G", 6);
linhasIndice.Add("H", 7);
linhasIndice.Add("I", 8);
linhasIndice.Add("J", 9);

Dictionary<String, int> colunasIndice = new Dictionary<String, int>();
colunasIndice.Add("1", 0);
colunasIndice.Add("2", 1);
colunasIndice.Add("3", 2);
colunasIndice.Add("4", 3);
colunasIndice.Add("5", 4);
colunasIndice.Add("6", 5);
colunasIndice.Add("7", 6);
colunasIndice.Add("8", 7);
colunasIndice.Add("9", 8);
colunasIndice.Add("10", 9);

//Definição embarcações (tamanho e quantidade)
Dictionary<String, int> tamanhoEmbarcacao = new Dictionary<String, int>();
tamanhoEmbarcacao.Add("PS", 5);
tamanhoEmbarcacao.Add("NT", 4);
tamanhoEmbarcacao.Add("DS", 3);
tamanhoEmbarcacao.Add("SB", 2);

Dictionary<String, int> embarcacoesQtd = new Dictionary<String, int>();
embarcacoesQtd.Add("PS", 1);
embarcacoesQtd.Add("NT", 2);
embarcacoesQtd.Add("DS", 3);
embarcacoesQtd.Add("SB", 4);

//variáveis
List<String> listaJogadores = new List<string>();
int qtdMaxJogadores = 2;
String[,] posicionamentoJogador1 = new String[10, 10];
String[,] posicionamentoJogador2 = new String[10, 10];
String[,] tabuleiroJogador1 = new String[10, 10];
String[,] tabuleiroJogador2 = new String[10, 10];


void IniciarJogo()
{
    Boolean inputValido = false;
    int opcaoInicial = 0;

    Console.WriteLine("====================== BATALHA NAVAL ======================\n");

    while (!inputValido)
    {
        Console.WriteLine("Menu inicial:\n- Digite 1 para jogar com outro jogador;\n- Digite 2 para jogar com o computador;\n- Digite 3 para sair.");

        inputValido = (int.TryParse(Console.ReadLine(), out opcaoInicial) && opcaoInicial >= 1 && opcaoInicial <= 3);

        if (inputValido)
        {
            break;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Digite uma opção válida.\n");
        }
    }

    if (opcaoInicial == 1)
    {
        Console.Clear();
        CadastrarJogadores();
        ConfigurarJog1();
        Console.Clear();
        Thread.Sleep(1000);
        ConfigurarJog2();
        Console.Clear();
        Thread.Sleep(1000);
        IniciarBatalha();
    }
    else if (opcaoInicial == 2)
    {
        JogarComputador();
    }
    else
    {
        Console.WriteLine("Precione '0' para fechar o console..");
        Environment.Exit(0);
    }
}

void CadastrarJogadores()
{
    int contador = 0;

    while (contador < qtdMaxJogadores)
    {
        Console.WriteLine($"Digite o nome do {(contador + 1)} jogador:");
        String nomeJogador = Console.ReadLine();
        listaJogadores.Add(nomeJogador);
        contador++;
    }
    Console.Clear();
}

void ConfigurarJog1()
{
    int totalPS = 1;
    int totalNT = 2;
    int totalDS = 3;
    int totalSB = 4;

    Boolean tudoPosicionado = false;

    while (!tudoPosicionado)
    {
        Console.WriteLine($"{listaJogadores[0]}, você possui as seguintes embarcações para posicionar:\n{totalPS} porta-aviões(PS);\n{totalNT} navios-tanque(NT);\n{totalDS} destroyers(DS);\n{totalSB} submarinos(SB).\n");
        ImprimirPosicionamento(posicionamentoJogador1);
        Console.WriteLine("\nQual embarcação você gostaria de posicionar?");
        String embarcacao = Console.ReadLine().Trim().ToUpper();

        if (!embarcacoesQtd.ContainsKey(embarcacao))
        {
            Console.Clear();
            Console.WriteLine($"====== TIPO INVÁLIDO DE EMBARCAÇÃO! DIGITE: PS, NT, DS ou SB. ======\n");
        }
        else if ((embarcacao == "PS" && totalPS == 0) || (embarcacao == "NT" && totalNT == 0) ||
                    (embarcacao == "DS" && totalDS == 0) || (embarcacao == "SB" && totalSB == 0))
        {
            Console.Clear();
            Console.WriteLine($"====== QUANTIDADE MÁXIMA DE '{embarcacao}' ATINGIDA. ======\n");
        }
        else
        {
            Console.WriteLine("Qual o posicionamento da embarcação?");
            String posicao = Console.ReadLine().ToUpper();
            VerificarPosicionamentoJog1(embarcacao, posicao, posicionamentoJogador1);

            if (VerificarPosicionamentoJog1(embarcacao, posicao, posicionamentoJogador1))
            {
                Console.WriteLine("====== EMBARCAÇÃO POSICIONADA ======\n");
                PosicionarJog1(posicionamentoJogador1, posicao);

                switch (embarcacao)
                {
                    case "PS":
                        totalPS--;
                        break;

                    case "NT":
                        totalNT--;
                        break;

                    case "DS":
                        totalDS--;
                        break;

                    case "SB":
                        totalSB--;
                        break;
                }
            }
        }

        if (totalPS == 0 && totalNT == 0 && totalDS == 0 && totalSB == 0)
        {
            tudoPosicionado = true;
        }
    }
}

void ConfigurarJog2()
{
    int totalPS = 1;
    int totalNT = 2;
    int totalDS = 3;
    int totalSB = 4;

    Boolean tudoPosicionado = false;

    while (!tudoPosicionado)
    {
        Console.WriteLine($"{listaJogadores[1]}, você possui as seguintes embarcações para posicionar:\n{totalPS} porta-aviões(PS);\n{totalNT} navios-tanque(NT);\n{totalDS} destroyers(DS);\n{totalSB} submarinos(SB).\n");
        ImprimirPosicionamento(posicionamentoJogador2);
        Console.WriteLine("\nQual embarcação você gostaria de posicionar?");
        String embarcacao = Console.ReadLine().Trim().ToUpper();

        if (!embarcacoesQtd.ContainsKey(embarcacao))
        {
            Console.WriteLine($"====== TIPO INVÁLIDO DE EMBARCAÇÃO! DIGITE: PS, NT, DS ou SB. ======\n");
        }
        else if ((embarcacao == "PS" && totalPS == 0) || (embarcacao == "NT" && totalNT == 0) ||
                    (embarcacao == "DS" && totalDS == 0) || (embarcacao == "SB" && totalSB == 0))
        {
            Console.Clear();
            Console.WriteLine($"====== QUANTIDADE MÁXIMA DE '{embarcacao}' ATINGIDA. ======\n");
        }
        else
        {
            Console.WriteLine("Qual o posicionamento da embarcação?");
            String posicao = Console.ReadLine().ToUpper();
            VerificarPosicionamentoJog2(embarcacao, posicao, posicionamentoJogador2);

            if (VerificarPosicionamentoJog2(embarcacao, posicao, posicionamentoJogador2))
            {
                Console.WriteLine("====== EMBARCAÇÃO POSICIONADA ======\n");
                PosicionarJog2(posicionamentoJogador2, posicao);
                switch (embarcacao)
                {
                    case "PS":
                        totalPS--;
                        break;

                    case "NT":
                        totalNT--;
                        break;

                    case "DS":
                        totalDS--;
                        break;

                    case "SB":
                        totalSB--;
                        break;
                }
            }
        }

        if (totalPS == 0 && totalNT == 0 && totalDS == 0 && totalSB == 0)
        {
            tudoPosicionado = true;
        }

    }
}

Boolean VerificarDisponibilidade(String[,] tabuleiro, String posicao)
{
    String primeiraLinha = posicao.Substring(0, 1);
    String primeiraColuna = posicao.Substring(1, 1);
    String ultimaLinha = posicao.Substring(2, 1);
    String ultimaColuna = posicao.Substring(3);

    int indexPrimeiraLinha = linhasIndice[primeiraLinha];
    int indexUltimaLinha = linhasIndice[ultimaLinha];
    int indexPrimeiraColuna = colunasIndice[primeiraColuna];
    int indexUltimaColuna = colunasIndice[ultimaColuna];

    int intervaloLinha = (indexUltimaLinha - indexPrimeiraLinha) + 1;
    int intervaloColuna = (indexUltimaColuna - indexPrimeiraColuna) + 1;

    if (primeiraLinha == ultimaLinha)
    {
        for (int i = indexPrimeiraColuna; i <= indexUltimaColuna; i++)
        {
            if (tabuleiro[indexPrimeiraLinha, i] != null)
            {
                return false;
            }
        }
    }
    else if (primeiraColuna == ultimaColuna)
    {
        for (int i = indexPrimeiraLinha; i <= indexUltimaLinha; i++)
        {
            if (tabuleiro[i, indexPrimeiraColuna] != null)
            {
                return false;
            }
        }
    }
    return true;
}

void PosicionarJog1(String[,] tabuleiro, String posicao)
{
    String primeiraLinha = posicao.Substring(0, 1);
    String primeiraColuna = posicao.Substring(1, 1);
    String ultimaLinha = posicao.Substring(2, 1);
    String ultimaColuna = posicao.Substring(3);

    int indexPrimeiraLinha = linhasIndice[primeiraLinha];
    int indexUltimaLinha = linhasIndice[ultimaLinha];
    int indexPrimeiraColuna = colunasIndice[primeiraColuna];
    int indexUltimaColuna = colunasIndice[ultimaColuna];

    if (primeiraLinha == ultimaLinha)
    {
        for (int i = indexPrimeiraColuna; i <= indexUltimaColuna; i++)
        {
            tabuleiro[indexPrimeiraLinha, i] = " 1";
        }
    }
    else if (primeiraColuna == ultimaColuna)
    {
        for (int i = indexPrimeiraLinha; i <= indexUltimaLinha; i++)
        {
            tabuleiro[i, indexPrimeiraColuna] = " 1";
        }
    }
}

void PosicionarJog2(String[,] tabuleiro, String posicao)
{
    String primeiraLinha = posicao.Substring(0, 1);
    String primeiraColuna = posicao.Substring(1, 1);
    String ultimaLinha = posicao.Substring(2, 1);
    String ultimaColuna = posicao.Substring(3);

    int indexPrimeiraLinha = linhasIndice[primeiraLinha];
    int indexUltimaLinha = linhasIndice[ultimaLinha];
    int indexPrimeiraColuna = colunasIndice[primeiraColuna];
    int indexUltimaColuna = colunasIndice[ultimaColuna];

    if (primeiraLinha == ultimaLinha)
    {
        for (int i = indexPrimeiraColuna; i <= indexUltimaColuna; i++)
        {
            tabuleiro[indexPrimeiraLinha, i] = " 1";
        }
    }
    else if (primeiraColuna == ultimaColuna)
    {
        for (int i = indexPrimeiraLinha; i <= indexUltimaLinha; i++)
        {
            tabuleiro[i, indexPrimeiraColuna] = " 1";
        }
    }
}

Boolean VerificarPosicionamentoJog1(String tipoEmbarcacao, String posicao, String[,] tabuleiro)
{
    Console.Clear();
    String primeiraLinha = posicao.Substring(0, 1);
    String primeiraColuna = posicao.Substring(1, 1);
    String ultimaLinha = posicao.Substring(2, 1);
    String ultimaColuna = posicao.Substring(3);

    int indexPrimeiraLinha = linhasIndice[primeiraLinha];
    int indexUltimaLinha = linhasIndice[ultimaLinha];
    int indexPrimeiraColuna = colunasIndice[primeiraColuna];
    int indexUltimaColuna = colunasIndice[ultimaColuna];

    int intervaloLinha = (indexUltimaLinha - indexPrimeiraLinha) + 1;
    int intervaloColuna = (indexUltimaColuna - indexPrimeiraColuna) + 1;

    if ((primeiraLinha == ultimaLinha && intervaloColuna == tamanhoEmbarcacao[tipoEmbarcacao]) || (primeiraColuna == ultimaColuna && intervaloLinha == tamanhoEmbarcacao[tipoEmbarcacao]))
    {
        if (VerificarDisponibilidade(tabuleiro, posicao))
        {
            return true;
        }
        else
        {
            Console.WriteLine("====== QUADRANTE JA OCUPADO, TENTE NOVAMENTE ======\n");
            return false;
        }
    }
    else
    {
        Console.WriteLine("====== NUMERO INVÁLIDO DE QUADRANTES PARA ESSE TIPO DE EMBARCAÇÃO ======\n");
        return false;
    }
    Console.Clear();
}

Boolean VerificarPosicionamentoJog2(String tipoEmbarcacao, String posicao, String[,] tabuleiro)
{
    Console.Clear();
    String primeiraLinha = posicao.Substring(0, 1);
    String primeiraColuna = posicao.Substring(1, 1);
    String ultimaLinha = posicao.Substring(2, 1);
    String ultimaColuna = posicao.Substring(3);

    int indexPrimeiraLinha = linhasIndice[primeiraLinha];
    int indexUltimaLinha = linhasIndice[ultimaLinha];
    int indexPrimeiraColuna = colunasIndice[primeiraColuna];
    int indexUltimaColuna = colunasIndice[ultimaColuna];

    int intervaloLinha = (indexUltimaLinha - indexPrimeiraLinha) + 1;
    int intervaloColuna = (indexUltimaColuna - indexPrimeiraColuna) + 1;

    if ((primeiraLinha == ultimaLinha && intervaloColuna == tamanhoEmbarcacao[tipoEmbarcacao]) || (primeiraColuna == ultimaColuna && intervaloLinha == tamanhoEmbarcacao[tipoEmbarcacao]))
    {
        if (VerificarDisponibilidade(tabuleiro, posicao))
        {
            return true;
        }
        else
        {
            Console.WriteLine("====== QUADRANTE JA OCUPADO, TENTE NOVAMENTE ======\n");
            return false;
        }
    }
    else
    {
        Console.WriteLine("====== NUMERO INVÁLIDO DE QUADRANTES PARA ESSE TIPO DE EMBARCAÇÃO ======\n");
        return false;
    }
    Console.Clear();
}

void JogarComputador()
{
    Console.Clear();
    Console.WriteLine("Opção indisponível no momento...\n\nAperte qualquer tecla para retornar ao menu principal.");
    Console.ReadKey();
    Console.Clear();
    IniciarJogo();
}

void ImprimirPosicionamento(string[,] tabuleiro)
{
    List<string> linhasCabecalho = new List<string>(10) { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
    Console.WriteLine("  1 2 3 4 5 6 7 8 9 10  ");
    for (int i = 0; i < 10; i++)
    {
        Console.Write(linhasCabecalho[i]);
        for (int j = 0; j < 10; j++)
        {
            if (tabuleiro[i, j] == null)
            {
                Console.Write(" 0");
            }
            else
            {
                Console.Write(tabuleiro[i, j]);
            }
        }
        Console.WriteLine();
    }
}

void ImprimirDisparos(string[,] tabuleiro)
{
    List<string> linhasCabecalho = new List<string>(10) { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
    Console.WriteLine("  1 2 3 4 5 6 7 8 9 10  ");
    for (int i = 0; i < 10; i++)
    {
        Console.Write(linhasCabecalho[i]);
        for (int j = 0; j < 10; j++)
        {
            if (tabuleiro[i, j] == null)
            {
                Console.Write("  ");
            }
            else
            {
                Console.Write(tabuleiro[i, j]);
            }
        }
        Console.WriteLine();
    }
}

void AtacarOponente(string posicao, String[,] tabuleiroPosicao, String[,] tabuleiroTiros)
{
    String linha = posicao.Substring(0, 1);
    String coluna = posicao.Substring(1);

    int indexLinha = linhasIndice[linha];
    int indexColuna = colunasIndice[coluna];

    if (!colunasIndice.ContainsKey(coluna) || !linhasIndice.ContainsKey(linha))
    {
        Console.WriteLine("Digite um valor dentro do intervalo pré-estabelecido.");
    }
    else if (tabuleiroTiros[indexLinha, indexColuna] != null)
    {
        Console.WriteLine("Você ja atirou nessa posição.. Aguarde sua vez novamente.");
    }
    else
    {
        if (tabuleiroPosicao[indexLinha, indexColuna] != null)
        {
            Console.WriteLine("\nTIRO CERTEIRO!!\n");
            tabuleiroTiros[indexLinha, indexColuna] = " X";
        }
        else
        {
            Console.WriteLine("\nVocê errou! Tente novamente após seu oponente atirar..\n");
            tabuleiroTiros[indexLinha, indexColuna] = " A";
        }
    }
}

Boolean AcertouOponente(string posicao, String[,] tabuleiroPosicao)
{
    String linha = posicao.Substring(0, 1);
    String coluna = posicao.Substring(1, 1);

    int indexLinha = linhasIndice[linha];
    int indexColuna = colunasIndice[coluna];

    if (tabuleiroPosicao[indexLinha, indexColuna] != null)
    {
        return true;
    }
    else
    {
        return false;
    }

}

void IniciarBatalha()
{
    int placarJog1 = 0;
    int placarJog2 = 0;
    Boolean finalizado = false;

    while (!finalizado)
    {
        Console.WriteLine($"\n{listaJogadores[0]} é a sua vez de atirar!\nQual a posição que você gostaria de atirar?");
        String tiroJog1 = Console.ReadLine().Trim().ToUpper();
        AtacarOponente(tiroJog1, posicionamentoJogador2, tabuleiroJogador2);
        ImprimirDisparos(tabuleiroJogador2);

        if (AcertouOponente(tiroJog1, posicionamentoJogador2))
        {
            placarJog1++;
        }
        if (placarJog1 == 30)
        {
            finalizado = true;
        }

        Console.WriteLine("\nAperte qualquer tecla para continuar.");
        Console.ReadKey();

        Console.Clear();

        Console.WriteLine($"\n{listaJogadores[1]} é a sua vez de atirar!\nQual a posição que você gostaria de atirar?");
        String tiroJog2 = Console.ReadLine().Trim().ToUpper();
        AtacarOponente(tiroJog2, posicionamentoJogador1, tabuleiroJogador1);
        ImprimirDisparos(tabuleiroJogador1);

        if (AcertouOponente(tiroJog2, posicionamentoJogador1))
        {
            placarJog2++;
        }
        if (placarJog2 == 30)
        {
            finalizado = true;
        }

        Console.WriteLine("\nAperte qualquer tecla para continuar.");
        Console.ReadKey();

        Console.Clear();

        Console.WriteLine("\n========== PLACAR ==========\n");
        Console.WriteLine($"{listaJogadores[0]}: {placarJog1} acertos\n{listaJogadores[1]}: {placarJog2} acertos\n");

        Console.WriteLine("\nAperte qualquer tecla para continuar.");
        Console.ReadKey();
        Console.Clear();
    }

    if (placarJog1 == 3)
    {
        Console.WriteLine($"Parabéns, {listaJogadores[0]}!! Você venceu a partida!!");
        Console.WriteLine($"\nVocê precisa treinar mais, {listaJogadores[1]}! Quem sabe numa próxima.. hahahah");
    }
    else
    {
        Console.WriteLine($"Parabéns, {listaJogadores[1]}!! Você venceu a partida!!");
        Console.WriteLine($"\nVocê precisa treinar mais, {listaJogadores[0]}! Quem sabe numa próxima.. hahahah");
    }

    Console.WriteLine("\nPressione qualquer tecla para retornar ao menu principal.");

    Console.ReadKey();

    Console.Clear();

    IniciarJogo();
}

IniciarJogo();