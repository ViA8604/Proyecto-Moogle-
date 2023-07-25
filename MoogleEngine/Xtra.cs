namespace MoogleEngine ;
using System ;
using System.Text.RegularExpressions ;

public class Xtra
{
    public static string Estandarizador(string archivos)
    {
    
    Regex rgx = new Regex( "[^a-zA-Z0-9*~!^áéíóú]");
    string filtered = rgx.Replace(archivos , " ");

    filtered = Regex.Replace(filtered , @"\s+" , " ");

    filtered = filtered.ToLower();

    return filtered;
    
    }

    public static int Contar (string [] arr , string word)           
    {
      //query devuelve cuantas veces esta una palabra en un array
      int conteo = 0 ; 

      for (int i = 0; i < arr.Length; i++) 
      {
        if(arr[i] == word)
        {
          conteo ++ ;
        }   
      }

      return conteo ;
    }

    public static int FrecMAX (string [] array)                      
    {
      //query halla mayor cantidad de repeticiones en un array
      int Maximo = 0 ;
      int rept = 0 ;

      for (int i = 0; i < array.Length; i++)
      {
        for (int j = 0; j < array.Length; j++)
        {
          if(array[i] == array[j])
          {
            rept ++ ;
          }   
        }    
        
        Maximo = Math.Max(Maximo , rept) ;
        rept = 0 ;
      }

      return Maximo;
    }

    
    public static void SelectionSort(Document [] array)
    {
      
      int n = array.Length;
      
      for (int i = 0; i < n - 1 ; i++)
      {
        int Maxindice = i;
        
        for (int j = i + 1; j < n; j++)
        {
        
          if(array[j].score > array[Maxindice].score)
          {
            Maxindice = j;
          }  
        } 
       
        (array[Maxindice] , array[i]) = (array[i] , array[Maxindice]) ; 
      }
    }
  

}

