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
var vetorPerfeito = false;
var caracterEspecialImpossivel = false;

var velocidades = new List<int>();

for (int i = 0; i < data.Count; i++)
{
    // System.Console.WriteLine(data[i].X + " - " + data[i].Y);

    if (i > 0)
        if ((data[i].X - data[i - 1].X) + (data[i].Y - data[i - 1].Y) != 0)
        {
            var velocidadeAtual = (data[i].X - data[i - 1].X) + (data[i].Y - data[i - 1].Y);
            // System.Console.WriteLine("Distancia movida: " + velocidadeAtual);
            velocidades.Add(velocidadeAtual);
        }
}
// verificando a velocidade 
var velAgrupado = velocidades.GroupBy(x => x);

// foreach (var item in velAgrupado)
// {
//     System.Console.WriteLine($"{item.Key} => {velocidades.Count(x => x == item.Key)}");
// }

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

// System.Console.WriteLine(velAgrupado);

















// deafult implementation example
// defeat instaclick bot
if (data.Count < 5 || velocidadeConstante || caracterEspecialImpossivel )
    isCracker();
else isUser();

void isCracker()
    => Console.WriteLine("Cracker");

void isUser()
    => Console.WriteLine("User");