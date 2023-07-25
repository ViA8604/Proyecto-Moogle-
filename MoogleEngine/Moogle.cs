using System;
using System.IO;


namespace MoogleEngine;

   

public static class Moogle
{

    public static SearchResult Query(string query) {
       
        query = Xtra.Estandarizador(query) ;
        Dataserver.BD.HacerBusqueda(query) ;

        SearchItem[] items = new SearchItem[3] ;

        
        for(int i = 0 ; i < 3 ; i++)       
        {
            Document now = Dataserver.BD.docs[i] ;

            items[i] = new SearchItem( now.TituloDoc() , now.Snippet() , now.score) ;
        }

        return new SearchResult(items, query);
    }
}
