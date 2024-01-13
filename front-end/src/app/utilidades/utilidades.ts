export function toBase64(file: File) {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = (error) => reject(error);
  });
}

export function parsearErroresAPI(response: any): string[] {
  console.log("parsearErrores",response);
  const resultado: string[] = [];

  if (response.error) {
    if (typeof response.error === 'string') {
      resultado.push(response.error);
    } else if (Array.isArray(response.error)){
      response.error.forEach(valor => resultado.push(valor.description));
    } else if(typeof response.error === 'object'){
    } else {
      const mapaErrores = response.error.errors;
      const entradas = Object.entries(mapaErrores);
      entradas.forEach((arreglo: any[]) => {
        const campo = arreglo[0];
        arreglo[1].forEach((mensajeError) => {
          resultado.push(`${campo}: ${mensajeError}`);
        });
      });
    }
  }

  return resultado;
}

export function formatearFecha(date: Date) {
  date = new Date(date);
  const formato = new Intl.DateTimeFormat('en', {
    year: 'numeric',
    month: '2-digit',
    day: '2-digit',
  });
  
  const [
      {value: month},,
      {value: day},,
      {value: year}
  ] = formato.formatToParts(date);

  return `${year}-${month}-${day}`;
}

export function dataURI(base64String: string): string {
  return `data:image/png;base64,${base64String}`;
}

export function onlySpecialchars(str)
{
  // Regex to check if a string
  // contains only special
  // characters
  var regex = /^[^a-zA-Z0-9]+$/;

  // If the string is empty
  // then print No
  if (str.length < 1) {
    document.write("No");
    return;
  }

  // Find match between given
  // string & regular expression
  var matchedAuthors = regex.test(str);

  // Print Yes If the string matches
  // with the Regex
  if (matchedAuthors) document.write("Yes");
  else document.write("No");
}

export function hasNumbers(t)
{
  var regex = /\d/g;
  return regex.test(t);
}    

export function hasUpperAndLowerCase(string:string){
  var i=0;
  var character='';
  var hasLowerCase = false;
  var hasUpperCase = false;
  while (i <= string.length){
      character = string.charAt(i);
      if (hasNumbers(character)){
          continue;
      }else{
          if (character == character.toUpperCase()) {
              hasUpperCase = true;
          }
          if (character == character.toLowerCase()){
              hasLowerCase = true;
          }
      }
      i++;
  }

  if(hasLowerCase && hasUpperCase)
    return true;

  return false;
}