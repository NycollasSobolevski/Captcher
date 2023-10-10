using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

var defaultJsons = new string[]
{
    "user-data 10.json",
    "user-data 16.json",
    "user-data 2.json"
};
var file = args.Length == 0 || !File.Exists(args[0]) ?
      defaultJsons[Random.Shared.Next(2)] : args[0];
List<UserData> data = UserData.Read(file);







// add code here
var velocidadeConstante = false;
var constanciaDigitacao = false;
var caracterEspecialImpossivel = false;
var vetorReto = false;

var velocidades = new List<int>();

for (int i = 0; i < data.Count; i++)
{
    if (i > 0)
        if ((data[i].X - data[i - 1].X) + (data[i].Y - data[i - 1].Y) != 0)
        {
            var velocidadeAtual = (data[i].X - data[i - 1].X) + (data[i].Y - data[i - 1].Y);
            velocidades.Add(velocidadeAtual);
        }
}
// verificando a velocidade 
var velAgrupado = velocidades.GroupBy(x => x);

foreach (var speed in velocidades.Distinct())
{
    if (
        velocidades.Count(x => x == speed) > velocidades.Count * 0.20
        && speed != 0 
        && Math.Abs(speed) != 1
    )
    {
        velocidadeConstante = true;
    }
}

//verificando repetições de inputs
var especialCharacters = new string[]{"!","@","#", "$","%","¨","&","(",")","_"};
for (int i = 0; i < data.Count; i++)
{
    if (i > 0)
    {
        if (especialCharacters.Contains(data[i].Text))
        {
            var current = i;

            while(current > -1)
            {
                if (data[current].Text != data[i].Text)
                {
                    caracterEspecialImpossivel = data[current].Text == "Shift" ? false : true;
                    
                    if (caracterEspecialImpossivel == false)
                        break;
                }
                current--;
            }       
        }
    }
}


// Velocidade digitação
var teclas = new List<string>();

for (int i = 0; i < data.Count; i++)
{
    if (data[i].Text != "")
        teclas.Add(data[i].Text);
}

var max = 0;
var repeated = 0;

foreach (var tecla in teclas.Distinct())
{
    if (teclas.Count(x => x == tecla) > max)
    {
        max = teclas.Count(x => x == tecla);
        repeated = 0;
    }
    else if (teclas.Count(x => x == tecla) == max)
        repeated++;
}

if (repeated >= teclas.Count * 0.5)
    constanciaDigitacao = true;

// Vetor
var angulos = new List<float>();

for (int i = 0; i < data.Count - 1; i++)
{
    if (
        data[i].X != 0 
        && data[i + 1].X != 0 
        && data[i].Y != 0 
        && data[i + 1].Y != 0
    )
    {
        var quadrant = 0;
        float angle = 0;

        var deltaX = (data[i].X - data[i + 1].X);
        var deltaY = (data[i].Y - data[i + 1].Y);

        double arcTg = (Math.Atan(deltaX/deltaY) * (180 / Math.PI));

        if (deltaX < 0 && deltaY < 0)
            quadrant = 2;

        if (deltaX > 0 && deltaY < 0)
            quadrant = 1;

        if (deltaX > 0 && deltaY > 0)
            quadrant = 4;

        if (deltaX < 0 && deltaY > 0)
            quadrant = 3;

        switch (quadrant)
        {
            case 1:
                angle = (float)(270 + -(arcTg));
                break;

            case 2:
                angle = (float)(270 - arcTg);
                break;

            case 3:
                angle = (float)(90 + -(arcTg));
                break;

            default:
                angle = (float)(90 - arcTg);
                break;           
        }

        angulos.Add(angle);
    }
}

var lists = new List<List<float>>();

var sequencia = new List<float>();

for (int i = 1; i < angulos.Count - 1; i++)
{
    if (angulos[i] == angulos[i + 1])
    {
        sequencia.Add(angulos[i]);
    }
    else 
    {
        sequencia.Add(angulos[i]);
        lists.Add(sequencia);
        sequencia.Clear();
    }
}

var maxSize = 0;

foreach (var lis in lists)
{
    if (lis.Count() > maxSize)
        maxSize = lis.Count();
}



// deafult implementation example
// defeat instaclick bot
if (data.Count < 80 || velocidadeConstante || caracterEspecialImpossivel || constanciaDigitacao || maxSize > 5)
    isCracker();
else isUser();

void isCracker()
    => Console.WriteLine("Cracker");

void isUser()
    => Console.WriteLine("User");