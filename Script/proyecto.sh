#! /bin/bash
# Funcion para ejecutar el moogle
run_moogle(){
    cd ./../moogle-main
    make dev
}

# Funcion para mostrar report y slide respect
show_report(){
    FILENAME="reporte"
    DIRECTORY="./../informe"
    show_pdf
}

show_slide(){
    FILENAME="presentacion"
    DIRECTORY="./../presentacion"
    show_pdf
}

# Funcion para crear report y slide respect.
crear_report(){
    FILENAME="reporte"
    DIRECTORY="./../informe"
    crear_pdf
}

crear_slide(){
    FILENAME="presentacion"
    DIRECTORY="./../presentacion"
    crear_pdf
}


#Funcion para crear los pdf a partir de los .tex
crear_pdf(){
    cd "${DIRECTORY}"
    pdflatex -interaction=batchmode -halt-on-error "${DIRECTORY}/${FILENAME}.tex" >/dev/null 2>&1
    pdflatex -interaction=batchmode -halt-on-error "${DIRECTORY}/${FILENAME}.tex" >/dev/null 2>&1
    echo "${FILENAME}.pdf creado con exito"
}


#Funcion para mostrar los pdf
show_pdf(){
    
    if [ -f "${DIRECTORY}/${FILENAME}.pdf" ]; then
        echo "Mostrando ${FILENAME}.pdf..."

        viewer=""
        if [ $# -eq 2 ]; then
         viewer="$2"
        fi

        if [ -z "$viewer" ]; then
            if command -v xdg-open &>/dev/null; then
                viewer="xdg-open"
                elif command -v open &>/dev/null; then
                viewer="open"
            fi
        fi 

        if [ -n "$viewer" ]; then
            $viewer "${DIRECTORY}/${FILENAME}.pdf"
            else
                echo "Lector de PDF no disponible"
        fi
        
         else
         echo "El archivo ${FILENAME}.pdf no existe"

        crear_pdf
        show_pdf
    fi
}

# Funcion para eliminar archivos auxiliares
clean_files(){
    
    cd ./../informe
    rm -f "reporte.aux" "reporte.log" "reporte.out" "reporte.synctex.gz" "reporte.toc" "reporte.fls" "reporte.fdb_latexmk" "reporte.idx"

    cd ./../presentacion
    rm -f "presentacion.aux" "presentacion.log" "presentacion.out" "presentacion.synctex.gz" "presentacion.toc" "presentacion.fls" "presentacion.fdb_latexmk" "presentacion.vrb" "presentacion.nav" "presentacion.snm"

    echo "Archivos auxiliares eliminados"
}





# Main
case "$1" in
    run)
        run_moogle ;;
    clean)
        clean_files ;;
    report)
        crear_report;;
    slide)
        crear_slide;;
    show_report)
        show_report;;
    show_slide)
        show_slide;;
    *)
        echo "Opción no valida. Por favor ingrese una de las siguientes opciones : run , clean , report , slide , show_report , show_slide";;
esac
