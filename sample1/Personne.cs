namespace sample1;

class Personne
{
  public string nom { get; set; }
  public int age { get; set; }

  public Personne(string nom, int age)
  {
    this.nom = nom;
    this.age = age;
  }

  public string Hello(bool isLowerCase)
  {
    string message = $"hello {this.nom}, you are {this.age} years old!";
    return isLowerCase ? message.ToLower() : message.ToUpper();
  }

}