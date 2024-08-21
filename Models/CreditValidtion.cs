using System.Text.RegularExpressions;

namespace JO_MOVIES.Models
{
    public class CreditValidtion
    {
        public string CardNumber { get; private set; }

        public CreditValidtion(string cardNumber)
        {
            CardNumber = cardNumber.Replace(" ", ""); // Remove any spaces
        }

        public string ValidateCard()
        {
            // Check if card number matches any known card types and is valid
            string cardType = GetCardType(CardNumber);

            if (cardType != "INVALID" && IsValidCardNumber(CardNumber))
            {
                return cardType;
            }
            else
            {
                return "INVALID";
            }
        }

        private bool IsValidCardNumber(string cardNumber)
        {
            // Use Luhn algorithm to validate the card number
            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(cardNumber[i].ToString());

                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                    {
                        n -= 9;
                    }
                }

                sum += n;
                alternate = !alternate;
            }

            return (sum % 10 == 0);
        }

        private string GetCardType(string cardNumber)
        {
            // Regular expressions for different card types
            var cardTypeRegexes = new (string Type, Regex Regex)[]
            {
            ("VISA", new Regex(@"^4[0-9]{12}(?:[0-9]{3})?$")),
            ("MASTERCARD", new Regex(@"^5[1-5][0-9]{14}$")),
            ("AMEX", new Regex(@"^3[47][0-9]{13}$")),
            ("DISCOVER", new Regex(@"^6(?:011|5[0-9]{2})[0-9]{12}$")),
            ("DINERS", new Regex(@"^3(?:0[0-5]|[68][0-9])[0-9]{11}$")),
            ("JCB", new Regex(@"^(?:2131|1800|35\d{3})\d{11}$"))
            };

            foreach (var cardType in cardTypeRegexes)
            {
                if (cardType.Regex.IsMatch(cardNumber))
                {
                    return cardType.Type;
                }
            }

            return "INVALID";
        }
    }
}
