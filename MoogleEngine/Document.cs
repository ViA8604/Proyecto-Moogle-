namespace MoogleEngine ;

using System ;
using System.IO ;

public class Document
{
    // Propiedades
    public string ruta ;  
    public Vector Documentow;
    public int maxfrec ;
    public double score;

    // Constructor
    public Document(string route)
    {                                 
        this.ruta = route ;
        this.Documentow = new Vector();

    }

    public string TituloDoc()
    {
        return this.ruta.Substring(this.ruta.LastIndexOf("/") + 1) ;
    }

    public string Snippet()
    {
        return "Snipet del documento(lo tengo que hacer)";
    }




}