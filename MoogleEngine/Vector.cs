using System ; 
using System.IO ;

namespace MoogleEngine ;

public class Vector
{
    // Propiedades
    public Dictionary <string , double> pesito = new Dictionary <string ,double> ();


    public double this[string d]
    {
        get { return pesito[d] ; } 

        set { pesito[d] = value ; } 
    }


    public static double SimilitudVect (Vector query , Vector doc)
    {
        double Escalar = 0.0;
        foreach (string word in query.pesito.Keys)
        {
                if (doc.pesito.ContainsKey(word))
                {
                    Escalar += query[word] * doc[word];
                }
                
        }
        return Escalar / (Vector.Norma(doc) * Vector.Norma(query)) ;
    }   

    public static double Norma (Vector A)
    {
        double sumatoria = 0.0 ;

        foreach(double number in A.pesito.Values)
        {
            sumatoria += Math.Pow(number , 2) ;
        }

        return Math.Sqrt(sumatoria) ;
    }



    }

    
    
 
    
    









