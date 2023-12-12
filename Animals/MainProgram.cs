


using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;



//=======================================================

public abstract class Animal
{
       public static long NumOfAnimals { get; set; }
        
       public int id { get; init; }

       public string name { get; init; }

       public bool isMale { get; init; }

       public double weight { get; init; }

       public DateTime birthDate { get; init; }

       public List<Product> Products { get; set; }

       public List<EnvironmentData> Environments { get; set; }
    
       public List<Cost> Costs { get; set; }

       public abstract double TotalStressFactor(EnvironmentData Env);

       public abstract int life();
 
       public abstract int timeToDie();

       public abstract double killPriority();

       public abstract int costPerDay();

       public abstract decimal IncomePerDay();

       public abstract decimal IncomeMeal(double priceMeat); 


       public override string ToString()
       {
           return $"Id: {id} Name: {name} IsMale: {isMale} Weight: {weight}Gr  Birth Date: {birthDate} Number Of Animals: {NumOfAnimals}";
       }
 }


//=======================================================


public class Sheep : Animal
{
    public Sheep(int id,string name,bool ismale,double weight,DateTime birtday)
    {
        NumOfAnimals++;
        NumOfSheeps++;
        this.weight = weight;
        this.id = id;
        this.name=name;
        this.isMale = ismale;
        this.birthDate = birtday;
    }

    public override double TotalStressFactor(EnvironmentData Env)
    {
        double stressfactor = 0;

        stressfactor = stressfactor + Env.db.stressFactor() + Env.tds.stressFactor() + Env.ox.stressFactor()
        + Env.temp.stressFactor() + Env.aqi.stressFactor() + Env.density.stressFactor();

        return Math.Round(stressfactor / 6, 2);
    }

    public override int life()
    {
        double life = 0;

        foreach (EnvironmentData Env in Environments)
        {
            life = life + TotalStressFactor(Env);
        }
        return (int)Math.Floor(life);
    }

    public override int timeToDie()
    {
        return (maxlife * 365) - life();
    }

    public override double killPriority()
    {
        double killPriority = 0;

        int tTod = (maxlife * 365) - life();

        switch (tTod)
        {
            case >= 7300:
                killPriority = 0;
                break;
            case int tTod1 when tTod1 < 7300 && tTod1 >= 5475:
                killPriority = 0.25;
                break;

            case int tTod1 when tTod1 < 5475 && tTod1 >= 3650:
                killPriority = 0.5;
                break;

            case int tTod1 when tTod1 < 3650 && tTod1 >= 1825:
                killPriority = 0.75;
                break;

            case int tTod1 when tTod1 < 1825 && tTod1 >= 0:
                killPriority = 1;
                break;
        }

        return killPriority;
    }

    public override int costPerDay()
    {
        double costday = 0;
        foreach(Cost Cs in Costs)
        {
            costday = costday + (Cs.dailyUsage * Cs.price);
        }

        return (int)costday;
    }

    public override decimal IncomePerDay()
    {
        decimal income = 0;
        foreach (Product pro in Products)
        {
            if (pro.producedInUnitsPerDay == 0)
                income = income + (decimal)(pro.PricePerUnit*pro.producedToUnit);
            else
                income = income + (decimal)(pro.producedInUnitsPerDay * pro.PricePerUnit);
        }
        return income;
    }

    public override decimal IncomeMeal(double priceMeat) => (decimal)(weight * priceMeat);
        

    public static int NumOfSheeps = 0;

    public static int maxlife { get; } = 12;

    List<SheepParameter> CowParameter = new List<SheepParameter>();

    public override string ToString()
    {
        return $"Name: {name} BirthDate: {birthDate} Age: {Math.Round((double)life() / 365, 0)} years old => Equivalent => {life()} days";
    }
}

//=====================================================

public class SheepParameter
{
    public int timeToStand { get; set; }
    public int timeToDeal { get; set; }
    public int timeToRelax { get; set; }
    
    public DateTime date {  get; set; }
}


//=======================================================

public class Cow : Animal
{
    public Cow(int id, string name, bool ismale, double weight, DateTime birtday)
    {
        NumOfAnimals++;
        NumOfCows++;
        this.weight = weight;
        this.id = id;
        this.name = name;
        this.isMale = ismale;
        this.birthDate = birtday;
    }

    public override double TotalStressFactor(EnvironmentData Env)
    {
        double stressfactor = 0;

        stressfactor = stressfactor + Env.db.stressFactor() + Env.tds.stressFactor() + Env.ox.stressFactor()
        + Env.temp.stressFactor() + Env.aqi.stressFactor() + Env.density.stressFactor();

        return Math.Round(stressfactor / 6, 2);
    }

    public override int life()
    {
        double life = 0;

        foreach (EnvironmentData Env in Environments)
        {
            life = life + TotalStressFactor(Env);
        }
        return (int)Math.Floor(life);
    }

    public override int timeToDie()
    {
        return (maxlife * 365) - life();
    }

    public override double killPriority()
    {
        double killPriority = 0;

        int tTod = (maxlife * 365) - life();

        switch (tTod)
        {
            case >= 7300:
                killPriority = 0;
                break;
            case int tTod1 when tTod1 < 7300 && tTod1 >= 5475:
                killPriority = 0.25;
                break;

            case int tTod1 when tTod1 < 5475 && tTod1 >= 3650:
                killPriority = 0.5;
                break;

            case int tTod1 when tTod1 < 3650 && tTod1 >= 1825:
                killPriority = 0.75;
                break;

            case int tTod1 when tTod1 < 1825 && tTod1 >= 0:
                killPriority = 1;
                break;
        }

        return killPriority;
    }

    public override int costPerDay()
    {
        double costday = 0;
        foreach (Cost Cs in Costs)
        {
            costday = costday + (Cs.dailyUsage * Cs.price);
        }

        return (int)costday;
    }

    public override decimal IncomePerDay()
    {
        decimal income = 0;
        foreach (Product pro in Products)
        {
            if (pro.producedInUnitsPerDay == 0)
                income = income + (decimal)(pro.PricePerUnit * pro.producedToUnit);
            else
                income = income + (decimal)(pro.producedInUnitsPerDay * pro.PricePerUnit);
        }
        return income;
    }

    public override decimal IncomeMeal(double priceMeat) => (decimal)(weight * priceMeat);


    public static int NumOfCows = 0;

    public static int maxlife { get; } = 25;

    List<CowParameter> CowParameter = new List<CowParameter>();

    public override string ToString()
    {
        return $"Name: {name} BirthDate: {birthDate} Age: {Math.Round((double)life() / 365, 0)} years old => Equivalent => {life()} days";
    }

}

//======================================================
public class CowParameter
{
    public int timeToStand { get; set; }
    public int timeToDeal {  get; set; }
    public int timeToRelax { get; set; }

    public DateTime date { get; set; }

    public override string ToString()
    {
        return $"TimeToStand: {timeToStand} TimeToDeal: {timeToDeal} TimeToRelax: {timeToRelax} DateTime: {date}";
    }
}

//=======================================================


public class EnvironmentData
{
    public HealthParameter<double> temp { get; set; }
    public HealthParameter<double> tds { get; set; }
    public HealthParameter<double> density { get; set; }
    public HealthParameter<double> db { get; set; }
    public HealthParameter<double> aqi { get; set; }
    public HealthParameter<double> ox { get; set; }

    public DateTime date { get; set; }

    public override string ToString()
    {
        return $"Temp: {temp} Tds: {tds} Density: {density} Db: {db} Aqi: {aqi} Ox: {ox} Date Time: {date}";
    }
}

//=======================================================


public class HealthParameter<T>
{
    public string name { get; set; }
    public T current { get; set; }
    public T standard { get; set; }

    public int alpha { get; set; }

    public double stressFactor()
    {
        double answ = (Convert.ToDouble(current) / Convert.ToDouble(standard)) * alpha;
        return Math.Round(answ, 2);
    }

    public override string ToString() => $"Name: {name} Standard: {standard} Current: {current}";
}

//=======================================================

public class Product
{
    public Product(string name,string unitOfMeasure, int producedInUnitsPerDay, int NoProduct,int producedToUnit, int PricePerUnit)
    {
        NoProduct++;

        this.name = name;
        this.unitOfMeasure= unitOfMeasure;
        this.producedInUnitsPerDay = producedInUnitsPerDay;
        this.NoProduct = NoProduct;
        this.producedToUnit = producedToUnit;
        this.PricePerUnit = PricePerUnit;
    }
    public int NoProduct { get; private set; } = 0;
    public string name { get; private set; }
    public string unitOfMeasure { get; private set; }
    public double producedInUnitsPerDay { get; private set; }
    public double producedToUnit { get; private set; }
    public double PricePerUnit { get; private set;}

    

    public override string ToString()
    {
        return $"Number Of Product: {NoProduct} Name: {name} Price per unit: {PricePerUnit}T  Produced in units per day: {producedInUnitsPerDay}{unitOfMeasure} Produced to unit: {producedToUnit}{unitOfMeasure}";
    }
}

//=======================================================


 public class Cost
 {
    public string name { get; set; }
    public string unitOfMeasure { get; set; }
    public double dailyUsage { get; set; }
    public double price { get; set; }

    public override string ToString()
    {
        return $"Name: {name}  Daily usage: {dailyUsage}{unitOfMeasure} Price: {price}T";
    }

}
