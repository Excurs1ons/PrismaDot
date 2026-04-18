namespace PrismaDot.Game.Perks;

public enum PerkTriggerType
{
    Passive,        // 被动常驻 (如：增加备弹)
    OnHit,          // 命中时
    OnKill,         // 击杀时
    OnReloadStart,  // 开始换弹时
    OnReloadFinish, // 换弹结束时
    OnCastSkill     // 释放技能时
}
