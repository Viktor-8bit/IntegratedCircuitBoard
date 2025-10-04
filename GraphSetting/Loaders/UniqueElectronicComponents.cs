namespace GraphSetting.Loaders;

public class UniqueElectronicComponents
{

    #region Компоненты
        // Буквально словарь Name -> ID 
        private Dictionary<string, int> _uniqueElectronicComponentsIDs = new Dictionary<string, int>();
        
        // Соединения в компонентах 
        public List<(string, string)> ElementsConnections { get; private set; }

        // счетчик ID компонентов
        public int CountUniqueElectronicComponents { get; private set; }
    #endregion
    
    #region Соединения
        // Словарь Network -> ID
        private Dictionary<string, int> _uniqueNetworks = new Dictionary<string, int>();

            
        // счетчик ID нетвороков
        public int CountUniqueNetworks { get; private set; }
    #endregion
    
    public UniqueElectronicComponents()
    {
        this.CountUniqueElectronicComponents = 0;
        this.CountUniqueNetworks = 0;
        this.ElementsConnections = new List<(string, string)>();
    }
    
    public int[,] MatrixR { get; private set; }
 
    // добавить девайс O(1)
    public void AddUniqueElectronicComponent(string uniqueElectronicComponent)
    {
        if (!this._uniqueElectronicComponentsIDs.ContainsKey(uniqueElectronicComponent))
        {
            this._uniqueElectronicComponentsIDs.Add(uniqueElectronicComponent, CountUniqueElectronicComponents);
            this.CountUniqueElectronicComponents++;
        }
    }

    // получить id девайса по его имени O(1)
    public int GetIDbyUniqueElectronicComponent(string uniqueElectronicComponent)
    {
        if (this._uniqueElectronicComponentsIDs.ContainsKey(uniqueElectronicComponent))
            return this._uniqueElectronicComponentsIDs[uniqueElectronicComponent];
        else
            throw new Exception("Не могу найти девайс");
    }

    // получить имя по ID O(n)
    public string? GetUniqueElectronicComponentById(int uniqueElectronicComponentId)
    {
        string? uniqueElectronicComponentName = null;
        foreach (var component in  this._uniqueElectronicComponentsIDs.Keys)
        {
            if (this._uniqueElectronicComponentsIDs[component].Equals(uniqueElectronicComponentId)) 
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
    public List<string> GetAllUniqueElectronicComponents() => this._uniqueElectronicComponentsIDs
                                                                .Keys.ToList();
    
    
    #region Матрица R
        // подготовить матрицу R
        public void MakeRMatrix()
        {
            // матрица R
            this.MatrixR = new int[_uniqueElectronicComponentsIDs.Count, _uniqueElectronicComponentsIDs.Count];

            foreach (var link in this.ElementsConnections)
            {
                var id1 = GetIDbyUniqueElectronicComponent(link.Item1);
                var id2 = GetIDbyUniqueElectronicComponent(link.Item2);
                
                this.MatrixR[id1, id2] += 1;
            }
        }
    #endregion

    #region Матица Q

        public void AddNetwork(string uniqueNetwork)
        {
            _uniqueNetworks.Add(uniqueNetwork, CountUniqueNetworks);
            this.CountUniqueNetworks += 1;
        }

        public string GetNetworkByID(int networkId)
        {
            var net = this._uniqueNetworks
                .Select(net => new { net.Key, net.Value })
                .Where(net => net.Value == networkId);

            if (net == null)
                throw new Exception("Не найдена network");
            
            return net.First().Key;
        }

        public List<string> GetAllNetworks() 
            => this._uniqueNetworks.Keys.ToList();
    
    #endregion
    
 
}