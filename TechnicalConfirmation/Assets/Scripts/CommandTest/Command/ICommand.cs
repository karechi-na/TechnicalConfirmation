/// <summary>
/// 各コマンドクラスが実装するインターフェース
/// </summary>
public interface ICommand
{
    void Execute();
    void Undo();
}
