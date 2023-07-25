namespace MoogleEngine ;

using System ;
using System.IO ;

public class Database
{
    public Document[] docs ;  // la lista de documentos de la base de datos.   

    //diccionario de diccionarios, contiene <palabra , <doc , count> >  . (Documentos donde esta la palabra y la cantidad de veces)
    public Dictionary <string , Dictionary <int , int> > contenido = new Dictionary <string , Dictionary <int , int> > () ;  

    public Vector quervector = new Vector ();   //futuro vector query
                                                              //^^^^^^^^^^^\\
    // -------------------------------------------------------- CONSTRUCTOR --------------------------------------------------------------------
    public void CargarBD(string folder)
    {
        /* Carga los documentos de la base de datos y los convierte en vectores S
        Se va a hacer antes de empezar el programa */

        CargarDocs(folder);
        FillDict();
        TFIDF(contenido) ;

    }

    public void HacerBusqueda (string query)
    {
        /* Recibe la consulta estandarizada y crea el vector query, despues lo compara con cada vector documento para hallar el score y ordena la 
        base de datos. (se hace despues de la consulta)
        */
        
        CrearVectorQuery(query.Split());

        // hace la similitud del coseno entre cada vector documento y el vector query
        for (int i = 0; i < docs.Length; i++)
        {
            docs[i].score = Vector.SimilitudVect(quervector , docs[i].Documentow);
        }
        
        Xtra.SelectionSort(docs);  // ordena la base de datos                
    }

    
    
    // ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ \\

    private void CargarDocs(string folder)
    {
        // Crea objetos tipo documento y guarda en cada uno su ruta
        folder = Path.Combine(Directory.GetCurrentDirectory() , ".." , folder) ;
        IEnumerable <string> Verarchivos = Directory.EnumerateFiles (folder , "*.txt" , SearchOption.AllDirectories) ;
        this.docs = new Document[Verarchivos.Count()] ; // crea un objeto documento por cada archivo que encuentre la funcion EnumerateFiles
       
        Parallel.For(0 , docs.Length , i => 
        {
            this.docs[i] = new Document(Verarchivos.ElementAt(i)) ; // (ver archivo Documents)            
        });

    }

    private void FillDict ()
    {
        /*Funcion que pone todas las palabras ya piola , y que llena el diccionario con: <doc en el que está, cant de veces> (ver foreach) 
        */

        for (int i = 0; i < docs.Length; i++)
        {
            string[] text = Xtra.Estandarizador( File.ReadAllText( docs[i].ruta)).Split() ;
                
            foreach (string word in text )
            {
                if ( ! this.contenido.ContainsKey(word) )
                {
                    this.contenido.Add( word , new Dictionary <int , int> ()  ) ;
                }
                
                if (! contenido[word].ContainsKey(i) )
                {
                    contenido[word].Add(i , 0);
                }
                
                contenido[word][i]++ ;
            }
        }
    }


    public void TFIDF (Dictionary<string , Dictionary<int , int>> contenido)
    {
        // Calcula el tf-idf de cada palabra  y crea los vectores documentos
        
        MaxFrecuency(contenido) ;   // LaPalabra
        foreach( string word in contenido.Keys)
        {
            double idf = Math.Log10( (docs.Length +1.0) / (contenido[word].Count() + 1.0 ));
            foreach (int num in contenido[word].Keys)
            {
                double tf = (double)contenido[word][num] / (docs[num].maxfrec+ 1.0) ;
                docs[num].Documentow[word] = tf * idf  ;
                
            }
        }    
    }   

    private void CrearVectorQuery (string [] query)                         
    {
        /*Pone en un array las palabras en query, para cada palabra halla su TFIDF y lo asigna a su dict pesito correspondiente ej: <au , 0,062383799>
        */

        foreach (string item in query)
        {

         if(contenido.ContainsKey(item))
         {
            double idf = Math.Log10((docs.Length) + 1.0) / (contenido[item].Count() + 1.0);
            double tf = Xtra.Contar(query , item) / (Xtra.FrecMAX(query) + 1.0) ;     
            quervector[item] = tf * idf ;
         }
            
         
        }
    }

    private void MaxFrecuency(Dictionary <string , Dictionary <int , int> > contenido)
    {
        // Calcula las veces que se repite la palabra mas comun en el doc(hace falta para el tf)

        foreach (string key in contenido.Keys)
        {
            foreach (int indice in contenido[key].Keys)
            {
                if (docs[indice].maxfrec < contenido[key][indice])
                {
                    docs[indice].maxfrec = contenido[key][indice] ;
                }

            }
        }     
    }

            
          
//--------------------------------------------------


}