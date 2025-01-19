namespace AddOnCore.Enums
{
    /// <summary>Варианты округления чисел</summary>
    public enum RoundMethods
    {
        /// <summary>Неустановлено</summary>
        Unknown,
        /// <summary>До 2 знаков после запятой</summary>
        TwoDigits,
        /// <summary>Округлять как сырье (три знака после запятой для погонажа; для остального - 4 знака</summary>
        Stuff,        
    }
}
