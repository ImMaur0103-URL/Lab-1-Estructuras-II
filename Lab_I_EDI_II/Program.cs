using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab_I_EDI_II
{
    class Program
    {


        static void Main(string[] args)
        {
            Console.SetWindowSize(135, 40);
            
            const string titulo1 = "█████████████       ████        ███         █████████	█████████████           ███     ███    ███     ███    █████████";
            const string titulo2 = "█████████████     ████████      ███         ███         █████████████           ███     ███    ███     ███    ███     ██";
            const string titulo3 = "    █████        ███	███     ███         ███             █████               ███     ███    ███     ███    ███     ██";
            const string titulo4 = "    █████       ███      ███    ███         ██████          █████               ███████████    ███     ███    █████████";
            const string titulo5 = "    █████       ███      ███    ███         ███             █████               ███     ███    ███     ███    ███";
            const string titulo6 = "    █████       ████████████	████████    ███             █████               ███     ███    ███     ███    ███";
            const string titulo7 = "    █████       ███      ███	████████    █████████       █████               ███     ███     █████████     ███";
            const string Titulo = titulo1 + "\n" + titulo2 + "\n" + titulo3 + "\n" + titulo3 + "\n" + titulo4 + "\n" + titulo5 + "\n" + titulo6 + "\n" + titulo7;

            const string opcion1 = "     Cargar CSV     ";
            const string opcion2 = "\n   Buscar por DPI   ";
            const string opcion3 = "\n  Buscar por nombre ";
            const string opcion4 = "\n Introducir Registro";
            const string opcion5 = "\n  Eliminar Registro ";

            const string opciones = opcion1 + opcion2 + opcion3 + opcion4 + opcion5;
            
            const string Separador = "\n-----------------------------------------------------------------------------------------------------------------------";

            Console.WriteLine(Titulo + Separador);

            IngresarCSV();
            while (true)
            {
                Opciones();
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            
            void Opciones()
            {
                try
                {

                    ConsoleKey consoleKey = new ConsoleKey();
                    int CursorOrg = 15; int Posicion = 9;
                    Console.CursorVisible = false;
                    do
                    {
                        Console.Clear();
                        switch (consoleKey)
                        {
                            case ConsoleKey.UpArrow:
                                Posicion--;

                                if (Posicion > 13) Posicion = 9;
                                else if (Posicion < 9) Posicion = 13;

                                break;
                            case ConsoleKey.DownArrow:
                                Posicion++;

                                if (Posicion > 13) Posicion = 9;
                                else if (Posicion < 9) Posicion = 13;
                                break;
                            default:
                                break;
                        }
                        Console.WriteLine(Titulo + Separador);
                        if (Posicion == 9) { Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.Black; }
                        else { Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.White; }
                        Console.Write(opcion1);
                        if (Posicion == 10) { Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.Black; }
                        else { Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.White; }
                        Console.Write(opcion2);
                        if (Posicion == 11) { Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.Black; }
                        else { Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.White; }
                        Console.Write(opcion3);
                        if (Posicion == 12) { Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.Black; }
                        else { Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.White; }
                        Console.Write(opcion4);
                        if (Posicion == 13) { Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.Black; }
                        else { Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.White; }
                        Console.Write(opcion5);
                        Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(Separador);
                        Console.SetCursorPosition(0, Posicion);
                    }
                    while ((consoleKey = Console.ReadKey().Key) != ConsoleKey.Enter);



                }
                catch (Exception e)
                {

                }
            }
        }

        static void IngresarCSV()
        {
            Console.WriteLine("Suba archivo .csv para continuar");
            string ruta = Console.ReadLine();

            if (ruta != "")
            {
                try
                {
                    StreamReader Lector = new StreamReader(ruta);
                    string linea;
                    while ((linea = Lector.ReadLine()) != null)
                    {
                        string linea1 = linea.Split(',')[0];
                        string linea2 = linea.Split(',')[1];
                        switch (linea1)
                        {
                            case "INSERT":
                                Console.WriteLine("Registro Cargado");
                                break;
                            case "DELETE":
                                break;
                            case "PATCH":
                                break;
                            default:
                                break;
                        }
                    }
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        

    }
}
