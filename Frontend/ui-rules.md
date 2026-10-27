# Reglas para aplicar cambios

- Antes de modificar los archivos, debes describir que modificacion hiciste en cada archivo.

# Comentarios

Solo comenta bloques de codigo relevantes.
Los comentarios solo responderan ¿Por que tomaste esta decisión?.

# Componentes React

- De acuerdo al diseño, siempre trata de buscar el componente correspondiende. Si no encontraste uno, entonces pregunta si puedes crear un nuevo componente o el nombre del componente.
- Nunca modifiques codigo que este relacionado con llamadas a APIs/Backend. Solo modifica codigo relacionado a la presentación.

# CSS

- Siempre usa los colores que esten en el :root de index.css. Si no encuentras algun color cercano, añade un color en :root con un comentario indicando que es el nuevo color que se creo.
- No uses :global
- Si no existe el archivo css respectivo con el nombre NombreComponente.module.css, entonces crea uno.

# Testing

- No compruebes los cambios en el Frontend.
- Siempre pide permiso para que puedas aplicar los cambios que vas a realizar en el archivo.
