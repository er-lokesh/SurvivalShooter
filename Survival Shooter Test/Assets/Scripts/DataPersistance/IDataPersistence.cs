public interface IDataPersistence
{
    void LoadData(GameData data);
    void SaveData(GameData data);
    void ClearData(GameData data);
}
