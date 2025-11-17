using System;
#nullable disable

// ====================================================================
// OUTPUT REASONING LEVEL 0 - QUESTION 1: Delegate Compilation Error
// ====================================================================
namespace OutputReasoningLevel0_Q1
{
    public class DelegateExample
    {
        public delegate void AuthCallback(bool validUser);
        
        public static AuthCallback loginCallback = Login;
        
        // Fixed: Login method now accepts a bool parameter to match delegate signature
        public static void Login(bool validUser)
        {
            Console.WriteLine("Valid user!");
        }
        
        public static void Run()
        {
            Console.WriteLine("\n=== OUTPUT REASONING LEVEL 0 - Q1 ===");
            loginCallback(true);
        }
    }
}

// ============================================================
// OUTPUT REASONING LEVEL 0 - QUESTION 2: Delegate with Lambda
// ============================================================
namespace OutputReasoningLevel0_Q2
{
    delegate void Notify(string msg);
    
    class DelegateLambdaExample
    {
        public static void Run()
        {
            Console.WriteLine("\n=== OUTPUT REASONING LEVEL 0 - Q2 ===");
            Notify handler = null;
            
            handler += (m) => Console.WriteLine("A: " + m);
            handler += (m) => Console.WriteLine("B: " + m.ToUpper());
            
            handler("hello");
            
            handler -= (m) => Console.WriteLine("A: " + m);
            handler("world");
        }
    }
}

// ============================================================
// OUTPUT REASONING LEVEL 1 - QUESTION 1: Exception Handling
// ============================================================
namespace OutputReasoningLevel1_Q1
{
    class ExceptionHandlingExample
    {
        static string txtAge;
        static DateTime selectedDate;
        static int parsedAge;
        
        public static void Run()
        {
            Console.WriteLine("\n=== OUTPUT REASONING LEVEL 1 - Q1 ===");
            try
            {
                Console.WriteLine(txtAge == null ? "txtAge is null" : txtAge);

                Console.WriteLine(selectedDate == default(DateTime)
                    ? "selectedDate is default"
                    : selectedDate.ToString());
                
                if (string.IsNullOrEmpty(txtAge))
                {
                    Console.WriteLine("txtAge is null or empty, cannot parse");
                }
                else
                {
                    parsedAge = int.Parse(txtAge);
                    Console.WriteLine($"Parsed Age: {parsedAge}");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Format Exception Caught");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("ArgumentNull Exception Caught");
            }
            finally
            {
                Console.WriteLine("Finally block executed");
            }
        }
    }
}

// ====================================================================
// OUTPUT REASONING LEVEL 1 - QUESTION 2: Multicast Delegate Exception
// ====================================================================
namespace OutputReasoningLevel1_Q2
{
    delegate void Operation();
    
    class MulticastDelegateExample
    {
        public static void Run()
        {
            Console.WriteLine("\n=== OUTPUT REASONING LEVEL 1 - Q2 ===");
            Operation ops = null;
            
            ops += Step1;
            ops += Step2;
            ops += Step3;
            
            try
            {
                ops();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught: " + ex.Message);
            }
            Console.WriteLine("End of Main");
        }
        
        static void Step1()
        {
            Console.WriteLine("Step 1");
        }
        
        static void Step2()
        {
            Console.WriteLine("Step 2");
            throw new InvalidOperationException("Step 2 failed!");
        }
        
        static void Step3()
        {
            Console.WriteLine("Step 3");
        }
    }
}

// ============================================================
// OUTPUT REASONING LEVEL 2 - QUESTION 1: Method Overloading
// ============================================================
namespace MethodOverloadingExample
{
    class MethodOverloading
    {
        public static void Run()
        {
            Console.WriteLine("\n=== OUTPUT REASONING LEVEL 2 - Q1 ===");
            int x = 5;
            new Base().F(x);
            new Derived().F(x);

            Console.ReadKey();
        }
    }
    
    class Base
    {
        public void F(int x)
        {
            Console.WriteLine("Base.F(int)");
        }
    }
    
    class Derived : Base
    {
        public void F(double x)
        {
            Console.WriteLine("Derived.F(double)");
        }
    }
}

// ============================================================
// OUTPUT REASONING LEVEL 2 - QUESTION 2: Event Handling
// ============================================================
namespace OutputReasoningLevel2_Q2
{
    class StepEventArgs : EventArgs
    {
        public int Step { get; }
        public StepEventArgs(int s) => Step = s;
    }
    
    class Workflow
    {
        public event EventHandler<StepEventArgs> StepStarted;
        public event EventHandler<StepEventArgs> StepCompleted;
        
        public void Run()
        {
            for (int i = 1; i <= 3; i++)
            {
                StepStarted?.Invoke(this, new StepEventArgs(i));
                Console.Write($"[{i}]");
                StepCompleted?.Invoke(this, new StepEventArgs(i));
            }
        }
    }
    
    class EventHandlingExample
    {
        public static void Run()
        {
            Console.WriteLine("\n=== OUTPUT REASONING LEVEL 2 - Q2 ===");
            Workflow wf = new Workflow();
            
            wf.StepStarted += (s, e) =>
            {
                Console.Write("<S" + e.Step + ">");
                if (e.Step == 2 && s != null)
                    ((Workflow)s).StepCompleted += (snd, ev)
                        => Console.Write("(Dyn" + ev.Step + ")");
            };
            wf.StepCompleted += (s, e) => Console.Write("<C" + e.Step + ">");
            
            wf.Run();
        }
    }
}

// ============================================================
// MAIN PROGRAM
// ============================================================
class OutputReasoningProgram
{
    static void Main()
    {
        Console.WriteLine("C# OUTPUT REASONING - ALL LEVELS");
        Console.WriteLine("=================================");
        
        OutputReasoningLevel0_Q1.DelegateExample.Run();
        OutputReasoningLevel0_Q2.DelegateLambdaExample.Run();
        OutputReasoningLevel1_Q1.ExceptionHandlingExample.Run();
        OutputReasoningLevel1_Q2.MulticastDelegateExample.Run();
        MethodOverloadingExample.MethodOverloading.Run();
        OutputReasoningLevel2_Q2.EventHandlingExample.Run();
        
        Console.WriteLine("\n\nPress any key to exit...");
        Console.ReadKey();
    }
}