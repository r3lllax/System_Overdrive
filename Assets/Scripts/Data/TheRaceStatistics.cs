using UnityEngine;

public static class TheRaceStatistics
{
    public static int KilledBasicEnemies; //Подключено
    public static int KilledRangedEnemies; //Подключено
    public static int KilledEliteEnemies; //Подключено
    public static int TimeAlive; //Подключено
    public static int KilledBoss; //Подключено
    public static int LostHearts; //Подключено
    public static int AbilityUsages; //Подключено
    public static int TimeInAbilities; //Подключено
    public static int PlayerLevel; //Подключено
    public static int BlockedBossUltiSlashes;  //Подключено

    public static void Reset()
    {
        KilledBasicEnemies = 0;
        KilledRangedEnemies = 0;
        KilledEliteEnemies = 0;
        TimeAlive = 0;
        KilledBoss = 0;
        LostHearts = 0;
        AbilityUsages = 0;
        TimeInAbilities = 0;
        PlayerLevel = 0;
        BlockedBossUltiSlashes = 0;
    }
    public static int GetScore()
    {
        int GarantedScore = 500 + ((PlayerLevel+1) * 200);
        int TimeScore = (int)(Mathf.Pow(TimeAlive, 1.5f) * 0.8f);
        int KillsScore = (KilledBasicEnemies * 10) + (KilledEliteEnemies * 50) + (KilledRangedEnemies * 30) + (KilledBoss * 300);
        int AbilityScore = (AbilityUsages * 500) + (TimeInAbilities * 50);
        float HeartsMultiplier;
        if (LostHearts < 1)
        {
            HeartsMultiplier = 1.2f;
        }
        else
        {
            HeartsMultiplier = 1.2f - (float)LostHearts / 15;
        }
        HeartsMultiplier = Mathf.Round(HeartsMultiplier * 100f) / 100f;
        int BlockedScore = BlockedBossUltiSlashes * 500;
        int FinalScore = (int)((GarantedScore + TimeScore + KillsScore + AbilityScore + BlockedScore) * HeartsMultiplier);
        GetDetails(GarantedScore,TimeScore,KillsScore,AbilityScore,BlockedScore,HeartsMultiplier,FinalScore);
        return FinalScore;
    }
    public static Statistics GetStatistics()
    {
        ////
        int GarantedScore = 500 + ((PlayerLevel + 1) * 200);
        int TimeScore = (int)(Mathf.Pow(TimeAlive, 1.5f) * 0.8f);
        int KillsScore = (KilledBasicEnemies * 10) + (KilledEliteEnemies * 50) + (KilledRangedEnemies * 30) + (KilledBoss * 300);
        int AbilityScore = (AbilityUsages * 500) + (TimeInAbilities * 50);
        float HeartsMultiplier;
        if (LostHearts < 1)
        {
            HeartsMultiplier = 1.2f;
        }
        else
        {
            HeartsMultiplier = 1.2f - (float)LostHearts / 15;
        }
        HeartsMultiplier = Mathf.Round(HeartsMultiplier * 100f) / 100f;
        int BlockedScore = BlockedBossUltiSlashes * 500;
        int FinalScore = (int)((GarantedScore + TimeScore + KillsScore + AbilityScore + BlockedScore) * HeartsMultiplier);
        ////
        Statistics gameStat = new Statistics();
        gameStat.GarantedScore = new string[] { $"{PlayerLevel + 1}", $"{PlayerLevel+1} x200 + 500", GarantedScore.ToString() };
        gameStat.BasicKills = new string[] { $"{KilledBasicEnemies}", $"{KilledBasicEnemies} x10", (KilledBasicEnemies * 10).ToString() };
        gameStat.RangedKills = new string[] { $"{KilledRangedEnemies}", $"{KilledRangedEnemies} x30", (KilledRangedEnemies * 30).ToString() };
        gameStat.EliteKills = new string[] { $"{KilledEliteEnemies}", $"{KilledEliteEnemies} x50", (KilledEliteEnemies * 50).ToString() };
        gameStat.BossKills = new string[] { $"{KilledBoss}", $"{KilledBoss} x300", (KilledBoss * 300).ToString() };
        gameStat.KillsSummary = $"{KillsScore}";
        gameStat.TimeScore = new string[] { $"{TimeAlive}", $"{TimeAlive}^1.5*0.8", ((int)(Mathf.Pow(TimeAlive, 1.5f) * 0.8f)).ToString() };
        gameStat.AbilitiesUsage = new string[] { $"{AbilityUsages}", $"{AbilityUsages} x500", (AbilityUsages * 500).ToString() };
        gameStat.AbilitiesTime = new string[] { $"{TimeInAbilities}", $"{TimeInAbilities} x50", (TimeInAbilities * 50).ToString() };
        gameStat.AbilitiesSummary = $"{AbilityScore}";
        gameStat.BossFight = new string[] { $"{BlockedBossUltiSlashes}", $"{BlockedBossUltiSlashes} x500", (BlockedBossUltiSlashes * 500).ToString() };
        gameStat.Defence = new string[] { $"{LostHearts}", $"1.2-{LostHearts}/15", (HeartsMultiplier).ToString() };
        gameStat.ClearExp = new string[] { "", "", (GarantedScore + TimeScore + KillsScore + AbilityScore + BlockedScore).ToString() };
        gameStat.ExpMultiplier = HeartsMultiplier.ToString();
        gameStat.CalculatedExp = new string[] { "", "", (FinalScore).ToString() };
        gameStat.Money = FinalScore / 10;
        return gameStat;
    }
    public static void GetDetails(int GarantedScore, int TimeScore, int KillsScore, int AbilityScore, int BlockedScore, float HeartsMultiplier, int FinalScore)
    {
        string result = $@"
        СТАТИСТИКА

        Гарантированные очки
        =======================================================
        Уровень                    {PlayerLevel + 1} x200 + 500  - {500 + ((PlayerLevel + 1) * 200)} оч.

        Убийства
        =======================================================
        Обычные враги            {KilledBasicEnemies} х10      -      {KilledBasicEnemies * 10} оч.
        Элитные враги            {KilledEliteEnemies} х50      -      {KilledEliteEnemies * 50} оч.
        Дальние враги            {KilledRangedEnemies} х30      -      {KilledRangedEnemies * 30} оч.
        Босс                     {KilledBoss} х300      -      {KilledBoss * 300} оч.
        Всего:                                     {(KilledBasicEnemies * 10) + (KilledEliteEnemies * 50) + (KilledRangedEnemies * 30) + (KilledBoss * 300)} оч.

        Время
        =======================================================
        Времени прожито           {TimeAlive} сек.^1.5*0.8 - {(int)(Mathf.Pow(TimeAlive, 1.5f) * 0.8f)} оч.

        Способности
        =======================================================
        Использовано способностей        {AbilityUsages} х500 - {AbilityUsages * 500} оч.
        Время использования              {TimeInAbilities} х50 - {TimeInAbilities * 50} оч.
        Всего за способности                      {(AbilityUsages * 500) + (TimeInAbilities * 100)} оч.

        Босс
        =======================================================
        Заблокировано ударов            {BlockedBossUltiSlashes} х500 - {BlockedBossUltiSlashes * 500} оч.

        Вы
        =======================================================
        Полученный урон                {LostHearts} ед. -  1.2-{LostHearts}/15 - {HeartsMultiplier} мн.

        Итого
        =======================================================
        Чистый опыт                                {GarantedScore + TimeScore + KillsScore + AbilityScore + BlockedScore} оч.
        Множитель за урон                             {HeartsMultiplier} х
        Итоговый счет                              {FinalScore} оч.
        Заработано денег             {FinalScore} оч./10 - {FinalScore / 10} монет
        ";
        Debug.Log(result);
    }

    ///Реализовать строковое возвращение информации типа
    /// Гарантированные очки
    /// ===================================================
    /// Уровень                    5 x200 + 500  - 1500 оч.
    /// 
    /// Убийства
    /// ===================================================
    /// Обычные враги            10 х10      -      100 оч.
    /// Элитные враги            10 х50      -      500 оч.
    /// Дальние враги            10 х30      -      300 оч.
    /// Босс                     1х 300      -      300 оч.
    /// Всего:                                     1200 оч.
    /// 
    /// Время
    /// ===================================================
    /// Времени прожито           64 сек.^1.5*0.8 - 410 оч.
    /// 
    /// Способности
    /// ===================================================
    /// Использовано способностей        14 х500 - 7000 оч.
    /// Время использования              120 х50 - 6000 оч.
    /// Всего за способности                      13000 оч.
    /// 
    /// Босс
    /// ===================================================
    /// Заблокировано ударов            30 х500 - 15000 оч.
    /// 
    /// Вы
    /// ===================================================
    /// Полученный урон                  3 ед./15 - 0.8 мн.
    /// 
    /// Итого
    /// ===================================================
    /// Чистый опыт                                31100оч.
    /// Множитель за урон                             0.8х
    /// Итоговый счет                              24888оч.
    /// Заработано денег          24888оч./10 - 24889 монет
}
