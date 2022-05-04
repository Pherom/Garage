using System;

namespace UI
{
    public class YesNoForm
    {
        private const string k_InvalidInputErrorMessage = "Invalid option picked";
        private const string k_NoDescisionMadeErrorMessage = "There was no decision";
        private string m_Input = null;
        private bool? m_Result = null;
        public bool Result
        {
            get
            {
                if (m_Result == null)
                {
                    throw new Exception(k_NoDescisionMadeErrorMessage);
                }

                return m_Result.Value;
            }
        }

        public void Display(string i_DisplayMessage)
        {
            while (m_Result == null)
            {
                try
                {
                    bool requestedNewGame = true;
                    Console.WriteLine(i_DisplayMessage);
                    m_Input = Console.ReadLine();
                    if (!(m_Input.ToLower().Equals("y") || m_Input.ToLower().Equals("n")))
                    {
                        throw new Exception(k_InvalidInputErrorMessage);
                    }
                    m_Result = m_Input.ToLower().Equals("y") ? requestedNewGame : !requestedNewGame;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public bool DisplayAndGetResult(string i_DisplayMessage)
        {
            Display(i_DisplayMessage);
            return m_Result.Value;
        }

        public void ResetForm()
        {
            m_Result = null;
        }
    }
}
