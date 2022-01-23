using UnityEngine;

[RequireComponent(typeof(PlayerControl))]
[RequireComponent(typeof(Animator))]

public class PlayerMoveAnimator : MonoBehaviour
{
    [Header("Animator Parameters")]
    public string MoveXFloatParameter = "MoveX";
    int MoveXHash;
    public string MoveZFloatParameter = "MoveZ";
    int MoveZHash;
    public string PoseIntParameter = "Pose";
    int PoseHash;

    PlayerControl Player;
    Animator Anim;

    private void Awake()
    {
        Player = GetComponent<PlayerControl>();
        Anim = GetComponent<Animator>();

        MoveXHash = Animator.StringToHash(MoveXFloatParameter);
        MoveZHash = Animator.StringToHash(MoveZFloatParameter);
        PoseHash = Animator.StringToHash(PoseIntParameter);
    }

    public void UpdateMovement(Vector2 move) => UpdateMovement(move.x, move.y);
    public void UpdateMovement(float x, float y)
    {
        if (Anim == null)
            return;

        Anim.SetFloat(MoveXHash, x);
        Anim.SetFloat(MoveZHash, y);
    }

    public void UpdatePose(int index)
    {
        if (Anim == null)
            return;

        Anim.SetInteger(PoseHash, index);
    }
}
