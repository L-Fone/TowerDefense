namespace ET
{
    public class GlobalProto
    {
        public string AssetBundleServerUrl;
        public string Address;

        public string LocalAssetBundleServerUrl;
        public string LocalAddress;

        public bool isLocal;

        public bool isEditorMode;

        public string ClientVersion;
        public string GetAddress()
        {
            return isLocal ?LocalAddress:Address;
        }
        public string GetUrl()
        {
            string url = isLocal ? LocalAssetBundleServerUrl : this.AssetBundleServerUrl;

            return url;
        }
    }
}
