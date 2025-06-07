using TMPro;
using UnityEngine;

public class EndGameStats : MonoBehaviour
{
    private Statistics gameStat;
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI PlayerLevel;
    [SerializeField] private TextMeshProUGUI PlayerLevelCalc;
    [SerializeField] private TextMeshProUGUI PlayerLevelScore;
    [SerializeField] private TextMeshProUGUI BasicKills;
    [SerializeField] private TextMeshProUGUI BasicKillsCalc;
    [SerializeField] private TextMeshProUGUI BasicKillsScore;
    [SerializeField] private TextMeshProUGUI RangedKills;
    [SerializeField] private TextMeshProUGUI RangedKillsCalc;
    [SerializeField] private TextMeshProUGUI RangedKillsScore;
    [SerializeField] private TextMeshProUGUI EliteKills;
    [SerializeField] private TextMeshProUGUI EliteKillsCalc;
    [SerializeField] private TextMeshProUGUI EliteKillsScore;
    [SerializeField] private TextMeshProUGUI BossKills;
    [SerializeField] private TextMeshProUGUI BossKillsCalc;
    [SerializeField] private TextMeshProUGUI BossKillsScore;
    [SerializeField] private TextMeshProUGUI KillsSummary;
    [SerializeField] private TextMeshProUGUI TimeAlive;
    [SerializeField] private TextMeshProUGUI TimeAliveCalc;
    [SerializeField] private TextMeshProUGUI TimeAliveScore;
    [SerializeField] private TextMeshProUGUI AbilitiesUsage;
    [SerializeField] private TextMeshProUGUI AbilitiesUsageCalc;
    [SerializeField] private TextMeshProUGUI AbilitiesUsageScore;
    [SerializeField] private TextMeshProUGUI AbilitiesTime;
    [SerializeField] private TextMeshProUGUI AbilitiesTimeCalc;
    [SerializeField] private TextMeshProUGUI AbilitiesTimeScore;
    [SerializeField] private TextMeshProUGUI AbilitiesSummary;
    [SerializeField] private TextMeshProUGUI BossBlock;
    [SerializeField] private TextMeshProUGUI BossBlockCalc;
    [SerializeField] private TextMeshProUGUI BossBlockScore;
    [SerializeField] private TextMeshProUGUI Defence;
    [SerializeField] private TextMeshProUGUI DefenceCalc;
    [SerializeField] private TextMeshProUGUI DefenceScore;
    [SerializeField] private TextMeshProUGUI ClearExp;
    [SerializeField] private TextMeshProUGUI ExpMultiplier;
    [SerializeField] private TextMeshProUGUI CalculatedExp;
    [SerializeField] private TextMeshProUGUI Money;



    [ContextMenu("Get")]
    public void GetStatistics()
    {
        gameStat = TheRaceStatistics.GetStatistics();
        SetData();
    }

    public void AddMoneyToPlayer()
    {
        DataManager.CurrentUser.Coins += gameStat.Money;

    }
    

    public void SetData()
    {
        //Блок гарантированных очков
        PlayerLevel.text = $"Уровень - {gameStat.GarantedScore[0]}";
        PlayerLevelCalc.text = $"{gameStat.GarantedScore[1]}";
        PlayerLevelScore.text = $"{gameStat.GarantedScore[2]} оч.";

        //Блок убитых врагов
        BasicKills.text = $"Обычные - {gameStat.BasicKills[0]}";
        BasicKillsCalc.text = $"{gameStat.BasicKills[1]}";
        BasicKillsScore.text = $"{gameStat.BasicKills[2]} оч.";

        RangedKills.text = $"Дальние - {gameStat.RangedKills[0]}";
        RangedKillsCalc.text = $"{gameStat.RangedKills[1]}";
        RangedKillsScore.text = $"{gameStat.RangedKills[2]} оч.";

        EliteKills.text = $"Элитные - {gameStat.EliteKills[0]}";
        EliteKillsCalc.text = $"{gameStat.EliteKills[1]}";
        EliteKillsScore.text = $"{gameStat.EliteKills[2]} оч.";

        BossKills.text = $"Босс - {gameStat.BossKills[0]}";
        BossKillsCalc.text = $"{gameStat.BossKills[1]}";
        BossKillsScore.text = $"{gameStat.BossKills[2]} оч.";

        KillsSummary.text = $"{gameStat.KillsSummary} оч.";

        //Блок Очков времени
        TimeAlive.text = $"Прожито - {gameStat.TimeScore[0]} сек.";
        TimeAliveCalc.text = $"{gameStat.TimeScore[1]}";
        TimeAliveScore.text = $"{gameStat.TimeScore[2]} оч.";

        //Блок очков способностей
        AbilitiesUsage.text = $"Использовано способностей - {gameStat.AbilitiesUsage[0]}";
        AbilitiesUsageCalc.text = $"{gameStat.AbilitiesUsage[1]}";
        AbilitiesUsageScore.text = $"{gameStat.AbilitiesUsage[2]} оч.";

        AbilitiesTime.text = $"Время использования - {gameStat.AbilitiesTime[0]}";
        AbilitiesTimeCalc.text = $"{gameStat.AbilitiesTime[1]}";
        AbilitiesTimeScore.text = $"{gameStat.AbilitiesTime[2]} оч.";

        AbilitiesSummary.text = $"{gameStat.AbilitiesSummary} оч.";

        //Блок босса
        BossBlock.text = $"Заблокировано рассечений - {gameStat.BossFight[0]}";
        BossBlockCalc.text = $"{gameStat.BossFight[1]}";
        BossBlockScore.text = $"{gameStat.BossFight[2]} оч.";

        //Блок защиты
        Defence.text = $"Получено урона - {gameStat.Defence[0]}";
        DefenceCalc.text = $"{gameStat.Defence[1]}";
        DefenceScore.text = $"{gameStat.Defence[2]} мн.";

        //Блок итогов
        ClearExp.text = $"{gameStat.ClearExp[2]} оч.";
        ExpMultiplier.text = $"{gameStat.ExpMultiplier}х";
        CalculatedExp.text = $"{gameStat.CalculatedExp[2]} оч.";
        Money.text = $"{gameStat.Money} монет.";


    }

}



public class Statistics
{
    public int Money;
    public string[] GarantedScore;
    public string[] BasicKills;
    public string[] RangedKills;
    public string[] EliteKills;
    public string[] BossKills;
    public string KillsSummary;
    public string[] TimeScore;
    public string[] AbilitiesUsage;
    public string[] AbilitiesTime;
    public string AbilitiesSummary;
    public string ExpMultiplier;
    public string[] BossFight;
    public string[] Defence;
    public string[] ClearExp;
    public string[] CalculatedExp;


}
