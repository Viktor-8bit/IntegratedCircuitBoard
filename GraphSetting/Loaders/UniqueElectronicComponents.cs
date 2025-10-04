namespace GraphSetting.Loaders;

public class UniqueElectronicComponents
{
    // Буквально словарь Name -> ID 
    private Dictionary<string, int> _uniqueElectronicComponentsIDs = new Dictionary<string, int>();
    
    // Соединения в компонентах 
    public List<(string, string)> ElementsConnections { get; private set; }
    
    // счетчик ID
    public int CountUniqueElectronicComponents { get; private set; }
    
    public UniqueElectronicComponents()
    {
        this.CountUniqueElectronicComponents = 0;
        this.ElementsConnections = new List<(string, string)>();
    }
    
    public int[,] MatrixR { get; private set; }
 
    // добавить девайс O(1)
    public void AddUniqueElectronicComponent(string uniqueElectronicComponent)
    {
        if (!this._uniqueElectronicComponentsIDs.ContainsKey(uniqueElectronicComponent))
        {
            this._uniqueElectronicComponentsIDs.Add(uniqueElectronicComponent, this.CountUniqueElectronicComponents);
            this.CountUniqueElectronicComponents++;
        }
    }

    // получить id девайса по его имени O(1)
    public int GetIDbyUniqueElectronicComponent(string uniqueElectronicComponent)
    {
        if (this._uniqueElectronicComponentsIDs.ContainsKey(uniqueElectronicComponent))
        {
            return this._uniqueElectronicComponentsIDs[uniqueElectronicComponent];
        }
        else
        {
            throw new Exception("Не могу найти девайс");
        }
    }

    // получить имя по ID O(n)
    public string? GetUniqueElectronicComponentById(int uniqueElectronicComponentID)
    {
        string? uniqueElectronicComponentName = null;
        foreach (var component in  this._uniqueElectronicComponentsIDs.Keys)
        {
            if (this._uniqueElectronicComponentsIDs[component].Equals(uniqueElectronicComponentID)) 
                uniqueElectronicComponentName = component;
        }
        
        if (uniqueElectronicComponentName == null)
            throw new Exception("Компонент null, а такого не должно быть в идеальном мире");

        return uniqueElectronicComponentName;
    }
    
    // добавить связь между элементами 
    public void AddElementConnection(string uniqueComponentName1, string uniqueComponentName2)
    {
        this.ElementsConnections.Add((uniqueComponentName1, uniqueComponentName2));
    }
    
    // получить все компоненты
    public List<string> GetAllUniqueElectronicComponents() => this._uniqueElectronicComponentsIDs.Keys.ToList();
    
    
    #region Матрица R
    
        // подготовить матрицу R
        public void MakeRMatrix()
        {
            // матрица R
            this.MatrixR = new int[this._uniqueElectronicComponentsIDs.Count, this._uniqueElectronicComponentsIDs.Count];

            foreach (var link in this.ElementsConnections)
            {
                var id1 = this.GetIDbyUniqueElectronicComponent(link.Item1);
                var id2 = this.GetIDbyUniqueElectronicComponent(link.Item2);
                
                this.MatrixR[id1, id2] += 1;
            }
        }
        
    #endregion

    #region Матица Q

    

    #endregion
    
 
}