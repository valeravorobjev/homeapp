namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Тип договора
    /// </summary>
    public enum ContractType: byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Прямая аренда
        /// </summary>
        VideoRent = 1,
        /// <summary>
        /// Субаренда
        /// </summary>
        SubRent = 2,
        /// <summary>
        /// Договор совместной деятельности
        /// </summary>
        JoinAgreement = 3,
        /// <summary>
        /// Закон 214 Ф3 (долевое участие)
        /// </summary>
        DU214F3 = 4,
        /// <summary>
        /// Договор ЖСК
        /// </summary>
        GSK = 5,
        /// <summary>
        /// Договор уступки прав требований
        /// </summary>
        AssignmentOfClaims = 6,
        /// <summary>
        /// Договор купли\продажи
        /// </summary>
        ContractOfSale= 7,
        /// <summary>
        /// Договор инвестирования
        /// </summary>
        InvestmentAgreement= 8
    }
}
