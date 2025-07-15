using Unity.Netcode.Components;

public class ClientNetworkAnimation : NetworkAnimator
{
    protected override bool OnIsServerAuthoritative() => false;
}
