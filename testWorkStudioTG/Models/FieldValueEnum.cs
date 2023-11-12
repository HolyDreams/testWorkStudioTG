namespace testWorkStudioTG.Models
{
    public class FieldValueEnum
    {
        public FieldValues FieldValue { get; }
        public FieldValueEnum(FieldValues fieldValue)
        {
            FieldValue = fieldValue;
        }

        public string Value()
        {
            switch (FieldValue)
            {
                case FieldValues.None:
                    return " ";
                case FieldValues.Zero:
                    return "0";
                case FieldValues.One:
                    return "1";
                case FieldValues.Two:
                    return "2";
                case FieldValues.Three: 
                    return "3";
                case FieldValues.Four:
                    return "4";
                case FieldValues.Five:
                    return "5";
                case FieldValues.Six:
                    return "6";
                case FieldValues.Seven:
                    return "7";
                case FieldValues.Eight:
                    return "8";
                case FieldValues.M:
                    return "M";
                case FieldValues.X:
                    return "X";
                default:
                    throw new Exception("Попытка получать неверное значение!");
            }
        }
        public enum FieldValues : byte
        {
            None,
            Zero,
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            M,
            X
        }
    }
}
