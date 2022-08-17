
const string storePassword = "ContraseniaSuperComplicada123";
Console.WriteLine("Validando Acceso");
Login();

void Login()
{
    int contadorDeIntentos = 0;
    bool loginExitoso = false;
    do
    {
        Console.WriteLine("Por favor ingrese su contraseña: ");
        string password = Console.ReadLine();

        if (storePassword == password)
        {
            loginExitoso = true;
        }
        else
        {
            Console.WriteLine("Password incorrecta");
        }
        contadorDeIntentos++;

        if (contadorDeIntentos > 5)
        {
            break;
        }
    }
    while (loginExitoso is false);

    if (loginExitoso)
    {
        string mensaje = ContadorDeCaracteres();
        Console.WriteLine(mensaje + " " + "Es correcta");
    }
    else
    {
        Console.WriteLine("No se ha podido logear.");
    }
}

string ContadorDeCaracteres()
{
    string resultado = String.Empty;
    for (int i = 0; i < storePassword.Length; i++)
    {
        resultado += "*";
    }

    return resultado;
}
