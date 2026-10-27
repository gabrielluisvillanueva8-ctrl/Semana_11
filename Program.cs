using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Semana11
{
    internal class Program
    {
        static int max_estudiantes = 40;
        static int max_cursos = 4;
        static string[] nombreCursos = { "Matematicas", "Programacion", "Fisica", "Base de datos" };
        static string[] Estudiante = new string[max_estudiantes];
        static double[,] matrizNotas = new double[max_estudiantes, max_cursos];
        static int total_estudiantes = 0;
        static string rutaArchivo = "notas_curso.txt";
        public static void RegistrarEstudiante()
        {
            Console.WriteLine("REGISTRO DE ESTUDIANTES");
            if (total_estudiantes >= max_estudiantes)
            {
                Console.WriteLine("Se ha alcanzado la capacidad maxima de estudiantes.");
                return;
            }
            string nombre = Validador.Leertexto("Ingrese el nombres y apellidos del estudiante: ").ToUpper();
            Estudiante[total_estudiantes] = nombre;
            Console.WriteLine("Ingresar las notas de 0 a 20:");
            for (int i = 0; i < max_cursos; i++)
            {
                matrizNotas[total_estudiantes, i] = Validador.LeerDecimales($"Ingresar nota de {nombreCursos[i]}: ");
            }
            total_estudiantes++;
            Console.WriteLine("Estudiantes registrado...!");
        }
        public static void guardararchivo()
        {
            Console.WriteLine("Guardar registros en archivos");
            if (total_estudiantes == 0)
            {
                Console.WriteLine("Sin Registros a guardar");
                return;
            }
            using (StreamWriter escritor = new StreamWriter(rutaArchivo))
            {
                for (int i = 0; i < total_estudiantes; i++)
                {
                    string linea = Estudiante[i];
                    for (int j = 0; j < max_cursos; j++)
                    {
                        linea += "," + matrizNotas[i, j];
                        //linea+=$",{ matrizNotas[i, j]}";
                    }
                    escritor.WriteLine(linea);
                }
            }
            Console.WriteLine("Registros guardados..!!");
        }
        public static void CargarRegistros()
        {
            Console.WriteLine("Cargar registros desde archivo");
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("No existe el archivo");
                return;
            }
            total_estudiantes = 0;
            using (StreamReader lector = new StreamReader(rutaArchivo))
            {
                string linea;
                while ((linea = lector.ReadLine()) != null && total_estudiantes < max_estudiantes)
                {
                    string[] partes = linea.Split(',');
                    if (partes.Length == max_cursos + 1)
                    {
                        Estudiante[total_estudiantes] = partes[0];
                        for (int i = 0; i < max_cursos; i++)
                        {
                            matrizNotas[total_estudiantes, i] = Convert.ToDouble(partes[i + 1]);
                        }
                        total_estudiantes++;
                    }
                }
            }
            Console.WriteLine("Se cargaron los registros del archivo..!!");
        }
        public static void MostrarRegistros()
        {
            Console.WriteLine(new string('=', 100));
            Console.WriteLine("\t\tREGISTRO GENERAL DE NOTAS POR CURSO");
            Console.WriteLine(new string('=', 100));
            if (total_estudiantes == 0)
            {
                Console.WriteLine("No existen registros");
                return;
            }
            Console.Write($"{"ESTUDIANTE",-25}|");
            for (int i = 0; i < max_cursos; i++)
            {
                Console.Write($"{nombreCursos[i],-12}|");
            }
            Console.WriteLine("PROMEDIO");
            Console.WriteLine(new string('-', 100));
            for (int i = 0; i < total_estudiantes; i++)
            {
                Console.Write($"{Estudiante[i],-25}|");
                double suma = 0;
                for (int j = 0; j < max_cursos; j++)
                {
                    Console.Write($"{matrizNotas[i, j],-12}|");
                    double nota = matrizNotas[i, j];
                    suma += nota;
                }
                double promedio = suma / max_cursos;
                string estado = promedio >= 12 ? "Aprobado" : "Desaprobado";
                Console.WriteLine($"{promedio:F2}({estado})");
            }
        }
        static void Main(string[] args)
        {
            int opc = 0;
            while (opc != 5)
            {
                Console.Clear();
                Console.WriteLine("1. REGISTRAR ESTUDIANTE");
                Console.WriteLine("2. GUARDAR EN ARCHIVO");
                Console.WriteLine("3. CARGAR REGISTROS DEL ARCHIVO");
                Console.WriteLine("4. MOSTRAR REGISTROS");
                Console.WriteLine("5. SALIR");
                opc = Validador.LeerEntero("Seleccionar una opcion: ");
                switch (opc)
                {
                    case 1:
                        RegistrarEstudiante();
                        break;
                    case 2:
                        guardararchivo();
                        break;
                    case 3:
                        CargarRegistros();
                        break;
                    case 4:
                        MostrarRegistros();
                        break;
                    case 5:
                        Console.WriteLine("Muchos exitos para el nuevo semestre...");
                        break;
                }
                Console.ReadKey();
            }
        }
    }
}
