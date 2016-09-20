namespace MindMap.Bussiness
{
    public static class PermissionBussiness
    {
        public static bool IsVip(int userId)
        {
            // TODO: check if user is vip
            return userId == 1;
        }
    }
}