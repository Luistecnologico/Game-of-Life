/**
 * Author: Luis Serra Jiménez
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Program
    {
        //Variables globales que se usaran a lo largo de todo el programa
        static int[,] gridPrincipal;
        static int[,] gridSecundario;
        static int contador = 0;
        public struct ventanaS
        {
            public int altura;
            public int anchura;
        }

        public static ventanaS ventana;
        //FIN Variables globales que se usaran a lo largo de todo el programa

         //INICIO DEL PROGRAMA, SOLO SE REALIZAN LLAMADAS
        static void Main(string[] args)
        {
            Inicializar();
        }

        /*
         * Metodo: Inicializar()
         * Descripción: Establece la dimensión del grid en función del tamaño por defecto de la pantalla.
         * Inicializa las variables.
         * 
         */
        static void Inicializar()
        {
            gridPrincipal = new int[Console.WindowWidth, Console.WindowHeight]; 
             gridSecundario = new int[Console.WindowWidth, Console.WindowHeight]; 
            ventana.altura = Console.WindowHeight; 
            ventana.anchura = Console.WindowWidth; 
            IniciarJuego();
        }

        /**
         * Metodo: IniciarJuego()
         * Descripción: Este metodo muestra el menu inicial y establece de manera automatica las celdas vivas iniciales
         * 
         */
        static void IniciarJuego()
        {
            int x, y, celdasVivasInicio;
            Console.WriteLine("Bienvenido al juego de la Vida");
            Console.Write("Seleccióne cuantas casillas quiere que empiecen vivas: ");
            celdasVivasInicio = int.Parse(Console.ReadLine());
            Console.Clear();
            TodosMuertos();
            CelulasIniciales(celdasVivasInicio);
            ImprimirCelulasVivas();
            Juego();
        }


        /**
         * Metodo: CelulasIniciales
         * Parametros: int contadorCelulas
         * Descripción: Establece en el grid las celulas vivas en función de lo establecido por el usuario antes
         * 
         */
        static void CelulasIniciales(int contadorCelulas)
        {
            Random random = new Random();
            int randomAltura;
            int randomAncho;

            for (int i = 1; i <= contadorCelulas; i++)
            {
                randomAltura = random.Next(0, (ventana.altura - 1)); //Modifico Para Debuj
                randomAncho = random.Next(0, (ventana.anchura - 1)); //Modifico Para Debuj
                if (EstadoCelula(randomAncho, randomAltura))
                    gridPrincipal[randomAncho, randomAltura] = 1;
                else
                    i--;
            }
        }


        /**
         * Metodo: EstadoCelula
         * Descripción: Devuelve verdadero si la celula se encuentra muerta, en caso de estar viva devuelve verdadero
         * 
         */
        static Boolean EstadoCelula(int anchura, int altura)
        {
            if (gridPrincipal[anchura, altura] == 0)
                return true;
            else
                return false;
        }

        /**
         * Metodo: TodosMuertos()
         * Descripción: Establece todo el grid a 0 (valor para las celulas muertas). El grid se establece primero poniendo el ancho y después la columna.
         * 
         */
        static void TodosMuertos()
        {
            for(int i = 0; i < ventana.anchura; i++)
            {
                for(int z= 0; z < ventana.altura; z++)
                {
                    gridPrincipal[i, z] = 0;
                    gridSecundario[i, z] = 0;
                }
            }
        }

        /**
         * Metodo: ImprimirCelulasVivas
         * Descripción: Imprime en pantalla las celulas Vivas
         */
        static void ImprimirCelulasVivas()
        {
            Console.Clear();
            for (int i = 0; i < ventana.anchura; i++)
            {
                for (int z = 0; z < ventana.altura; z++)
                {
                    if(!EstadoCelula(i,z))
                    {
                        Console.SetCursorPosition(i, z);
                        Console.Write("@");
                    }
                }
            }
        }


        /**
         * Metodo: MatarOResucitarCelula
         * param name="altura"
         * param name="anchura"
         * Descripción: Establece en el gridSecundario si la celula estara en el siguiente turno viva o muerta.
         */
        static void MatarOResucitarCelula(int anchura, int altura)
        {
            int celulasVivasPerimetro = ComprobarCelulasPerimetro(anchura, altura);
            
            if (!EstadoCelula(anchura, altura))
            {
                celulasVivasPerimetro--;
                if(celulasVivasPerimetro == 2 || celulasVivasPerimetro == 3)
                {
                    gridSecundario[anchura, altura] = 1;
                }
                else
                {
                    gridSecundario[anchura, altura] = 0;
                } 
            }
            else
            {
                if(celulasVivasPerimetro == 3)
                    gridSecundario[anchura, altura] = 1;
                else
                    gridSecundario[anchura, altura] = 0;
            }
        }


        /**
         * Metodo: ComprobarCelulasPerimetro
         * Descripción: Comprueba las celulas junto a la celula indicada para saber el numero de celulas vivas
         */
        static int ComprobarCelulasPerimetro(int anchura, int altura)
        {
            int contador = 0;

            for(int i = BordeInferiorAnchura(anchura); i <= BordeMaximoAnchura(anchura); i++)
            {
                for(int z = BordeInferiorAltura(altura); z <= BordeMaximoAltura(altura); z++)
                {
                    if (!EstadoCelula(i, z))
                        contador++;
                }
            }
            return contador;
        }

        static int BordeInferiorAnchura(int j)
        {
            if (j == 0)
                return j;
            else
                return (j - 1);
        }

        static int BordeMaximoAnchura(int j)
        {
            if (j == (ventana.anchura - 1))
                return j;
            else
                return (j + 1);
        }

        static int BordeInferiorAltura(int j)
        {
            if (j == 0)
                return j;
            else
                return (j - 1);
        }

        static int BordeMaximoAltura(int j)
        {
            if (j == (ventana.altura - 1))
                return j;
            else
                return (j + 1);
        }

        static void TodosMuertosSecundario()
        {
            for (int i = 0; i < ventana.anchura; i++)
            {
                for (int z = 0; z < ventana.altura; z++)
                {
                    gridSecundario[i, z] = 0;
                }
            }
        }

        static void CopiarSecundarioEnPrimario()
        {
            for (int i = 0; i < ventana.anchura; i++)
            {
                for (int z = 0; z < ventana.altura; z++)
                {
                    gridPrincipal[i, z] = gridSecundario[i, z];
                }
            }
        }

       static void ComprobarCelulas()
        {
            for (int i = 0; i < ventana.anchura; i++)
            {
                for (int z = 0; z < ventana.altura; z++)
                {
                    MatarOResucitarCelula(i,z);
                }
            }
        } 

        static void Juego()
        {
            while (true)
            {   
                ComprobarCelulas();
                CopiarSecundarioEnPrimario();
                TodosMuertosSecundario();
                ImprimirCelulasVivas();
                System.Threading.Thread.Sleep(100);
            }
        }

    }
}
