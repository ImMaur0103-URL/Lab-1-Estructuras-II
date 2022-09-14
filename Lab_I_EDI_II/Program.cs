using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using BTree;

namespace Lab_I_EDI_II
{
    public static class Extensions
    {
        public static string Filter(this string str, List<char> charsToRemove)
        {
            foreach (char c in charsToRemove)
            {
                str = str.Replace(c.ToString(), String.Empty);
            }

            return str;
        }
    }
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
            const string opcion4 = "\n introducir Registro";
            const string opcion5 = "\n  Eliminar Registro ";

            const string opciones = opcion1 + opcion2 + opcion3 + opcion4 + opcion5;

            //BTree<int, BaseDatos> Arbol = new BTree<int, BaseDatos>(5); Trabajo en Proceso Futura Actualizacion
            List<BaseDatos> baseDatos = new List<BaseDatos>();
            List<string> baseDatosDPI = new List<string>();
            List<string> baseDatosNombres = new List<string>();

            const string Separador = "\n-----------------------------------------------------------------------------------------------------------------------";

            Console.WriteLine(Titulo + Separador);

            IngresarCSV(ref baseDatos, ref baseDatosDPI, ref baseDatosNombres);//ref Arbol
            while (true)
            {
                Opciones();
                ConsoleKey Exit = new ConsoleKey();
                Console.WriteLine("Para salir preciona escape");
                if ((Exit = Console.ReadKey().Key) == ConsoleKey.Escape) break;
            }
            
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

                    Console.SetCursorPosition(0, 15);
                    Console.CursorVisible = true;

                    switch (Posicion) 
                    {
                        case 9:
                            IngresarCSV(ref baseDatos, ref baseDatosDPI, ref baseDatosNombres);//ref Arbol
                            break;
                        case 10:
                            BusquedaDPI(ref baseDatos, ref baseDatosDPI, ref baseDatosNombres);
                            break;
                        case 11:
                            BusquedaName(ref baseDatos, ref baseDatosDPI, ref baseDatosNombres);
                            break;
                        case 12:
                            introducir(ref baseDatos, ref baseDatosDPI, ref baseDatosNombres);
                            break;
                        case 13:
                            Eliminar(ref baseDatos, ref baseDatosDPI, ref baseDatosNombres);
                            break;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static void IngresarCSV(ref List<BaseDatos> Lista, ref List<string> ListaD, ref List<string> ListaN)//ref BTree<int, BaseDatos> Arbol
        {
            Console.WriteLine("Suba archivo .csv para continuar");
            string ruta = Console.ReadLine();

            if (ruta != "")
            {
                try
                {
                    StreamReader Lector = new StreamReader(ruta);
                    List<char> charsToRemove = new List<char>() { '"', '{', '}'};
                    BaseDatos baseDeDatos;
                    string linea;
                    while ((linea = Lector.ReadLine()) != null)
                    {
                        string linea1 = linea.Split(';')[0];
                        string linea2 = linea.Split(';')[1];
                        linea2 = linea2.Filter(charsToRemove);
                        int key = Convert.ToInt32(Convert.ToInt64(linea2.Split(',')[1].Split(':')[1]) / 5000) + (linea2.Split(',')[1].Split(':')[0].Length * 5000);
                        switch (linea1)
                        {
                            case "INSERT":
                                baseDeDatos = new BaseDatos(linea2.Split(',')[1].Split(':')[1], linea2.Split(',')[0].Split(':')[1], linea2.Split(',')[3].Split(':')[1], Convert.ToDateTime(linea2.Split(',')[2].Split(':')[1] + ":" + linea2.Split(',')[2].Split(':')[2]));//
                                Lista.Add(baseDeDatos);
                                ListaD.Add(baseDeDatos.DPI);
                                ListaN.Add(baseDeDatos.Nombre);
                                //Arbol.Insert(key, baseDeDatos);
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
        static void BusquedaDPI(ref List<BaseDatos> lista, ref List<string> ListaD, ref List<string> ListaN)//ref BTree<int, BaseDatos> bTree
        {
            Console.WriteLine("---Busqueda por DPI---");
            Console.Write("DPI: ");
            string DPI = Console.ReadLine();
            int posicion = ListaD.IndexOf(DPI);
            BaseDatos baseDatos = lista.ElementAt(posicion);

            string linea = baseDatos.GET();
            Console.WriteLine("\n" + linea.Split(",")[0]);
            Console.WriteLine(linea.Split(",")[1]);
            Console.WriteLine(linea.Split(",")[2]);
            Console.WriteLine(linea.Split(",")[3] + "\n");

            //Actualizacion tranajo con Arbol B
            //int key = Convert.ToInt32(Convert.ToInt64(DPI) / 5000) + (6 * 5000);
            //BaseDatos baseDatos = bTree.Search(key).Pointer;

        }
        static void BusquedaName(ref List<BaseDatos> lista, ref List<string> ListaD, ref List<string> ListaN)//ref BTree<int, BaseDatos> bTree
        {
            Console.WriteLine("---Busqueda por Nombre---");
            Console.Write("Nombre: ");
            string Nombre = Console.ReadLine();
            for (int i = 0; i < ListaN.Count; i++)
            {
                if(ListaN[i] == Nombre)
                {
                    BaseDatos baseDatos = lista.ElementAt(i);

                    Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------");

                    string linea = baseDatos.GET();
                    Console.WriteLine(linea.Split(",")[0]);
                    Console.WriteLine(linea.Split(",")[1]);
                    Console.WriteLine(linea.Split(",")[2]);
                    Console.WriteLine(linea.Split(",")[3]);


                    Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------");
                }
            }
        }
        static void introducir(ref List<BaseDatos> lista, ref List<string> ListaD, ref List<string> ListaN)//ref BTree<int, BaseDatos> bTree
        {
            Console.WriteLine("---introducir Registro---");
            Console.Write("DPI: ");
            string DPI = Console.ReadLine();
            Console.Write("Nombre: ");
            string Nombre = Console.ReadLine();
            Console.Write("Fecha de nacimeinto: ");
            string BornDate = Console.ReadLine();
            Console.Write("Direcciton: ");
            string address = Console.ReadLine();
            Console.WriteLine("\n---Nuevo Registro a crear---\n{DPI:" + DPI + ", Nombre:" + Nombre + ", Fecha de nacimiento:" + BornDate + ", Direccion:" + address + "}");

            BaseDatos baseDeDatos = new BaseDatos(DPI, Nombre, address, Convert.ToDateTime(BornDate));
            lista.Add(baseDeDatos);
            ListaD.Add(baseDeDatos.DPI);
            ListaN.Add(baseDeDatos.Nombre);
            Console.WriteLine("Registro Cargado");

        }
        static void Eliminar(ref List<BaseDatos> lista, ref List<string> ListaD, ref List<string> ListaN)//ref BTree<int, BaseDatos> bTree
        {
            Console.WriteLine("---Eliminar Registro---");
            Console.Write("DPI: ");
            string DPI = Console.ReadLine();
            Console.Write("Nombre: ");
            string Nombre = Console.ReadLine();

            int posicion = ListaD.IndexOf(DPI);
            BaseDatos baseDatos = lista.ElementAt(posicion);

            string linea = baseDatos.GET();
            Console.WriteLine("\n" + linea.Split(",")[0]);
            Console.WriteLine(linea.Split(",")[1]);
            Console.WriteLine(linea.Split(",")[2]);
            Console.WriteLine(linea.Split(",")[3] + "\n");

            Console.WriteLine("---Registro Eliminado---");
        }


    }
}
