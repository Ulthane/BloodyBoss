using BloodyBoss.DB.Models;
using BloodyBoss.DB;
using ProjectM;
using System.Linq;
using VampireCommandFramework;
using BloodyBoss.Exceptions;
using Bloody.Core.API.v1;
using System;

namespace BloodyBoss.Commands
{

    [CommandGroup("bb items")]
    internal class ItemsBoosCommand
    {

        [Command("list", usage: "<NameOfBoss>", description: "List items of Boss drop", adminOnly: true)]
        public void ListBossItems(ChatCommandContext ctx, string BossName)
        {

            try
            {
                if (Database.GetBoss(BossName, out BossEncounterModel boss))
                {
                    ctx.Reply($"{boss.name} Items List");
                    ctx.Reply($"----------------------------");
                    ctx.Reply($"--");
                    foreach (var item in boss.GetItems())
                    {
                        ctx.Reply($"Item {item.ItemID}");
                        ctx.Reply($"Stack {item.Stack}");
                        ctx.Reply($"Chance {item.Chance}");
                        ctx.Reply($"--");
                    }
                    ctx.Reply($"----------------------------");
                }
                else
                {
                    throw new BossDontExistException();
                }
            }
            catch (BossDontExistException)
            {
                var _message = Database.LOCALIZATIONS["MSG_Boss_Does_Not_Exist"];
                _message = _message.Replace("#boss#", FontColorChatSystem.Yellow(BossName));
                throw ctx.Error(_message);
            }
            catch (ProductExistException)
            {
                var _message = Database.LOCALIZATIONS["MSG_Item_Already_Exist"];
                _message = _message.Replace("#boss#", FontColorChatSystem.Yellow(BossName));
                throw ctx.Error(_message);
            }
            catch (Exception e)
            {
                throw ctx.Error($"Error: {e.Message}");
            }
            
        }

        // .encounter product add Test1 736318803 20
        [Command("add", usage: "<NameOfBoss> <ItemName> <ItemPrefabID> <Stack> <Chance>", description: "Add a item to a Boss drop. Chance is number between 0 to 1, Example 0.5 for 50% of drop", adminOnly: true)]
        public void CreateItem(ChatCommandContext ctx, string BossName, string ItemName, int ItemPrefabID, int Stack, int Chance)
        {
            try
            {
                if(Database.GetBoss(BossName, out BossEncounterModel Boss))
                {
                    Boss.AddItem(ItemName,ItemPrefabID, Stack, Chance);
                    var _message = Database.LOCALIZATIONS["MSG_Item_Added_Successfully"];
                    _message = _message.Replace("#boss#", FontColorChatSystem.Yellow(BossName));
                    ctx.Reply(_message);
                }
                else
                {
                    throw new BossDontExistException();
                }
            }
            catch (BossDontExistException)
            {
                var _message = Database.LOCALIZATIONS["MSG_Boss_Does_Not_Exist"];
                _message = _message.Replace("#boss#", FontColorChatSystem.Yellow(BossName));
                throw ctx.Error(_message);
            } 
            catch (ProductExistException)
            {
                var _message = Database.LOCALIZATIONS["MSG_Item_Already_Exist"];
                _message = _message.Replace("#boss#", FontColorChatSystem.Yellow(BossName));
                throw ctx.Error(_message);
            } 
            catch (Exception e)
            {
                throw ctx.Error($"Error: {e.Message}");
            }

            
        }

        // .encounter product remove Test1 736318803
        [Command("remove", usage: "<NameOfBoss> <ItemName>", description: "Remove a item to a Boss", adminOnly: true)]
        public void RemoveProduct(ChatCommandContext ctx, string BossName, string ItemName)
        {
            try
            {
                if (Database.GetBoss(BossName, out BossEncounterModel npc))
                {
                    npc.RemoveItem(ItemName);
                    var _message = Database.LOCALIZATIONS["MSG_Item_Removed_Succesfully"];
                    _message = _message.Replace("#boss#", FontColorChatSystem.Yellow(BossName));
                    ctx.Reply(_message);
                }
                else
                {
                    throw new BossDontExistException();
                }
            }
            catch (BossDontExistException)
            {
                var _message = Database.LOCALIZATIONS["MSG_Boss_Does_Not_Exist"];
                _message = _message.Replace("#boss#", FontColorChatSystem.Yellow(BossName));
                throw ctx.Error(_message);
            }
            catch (ProductDontExistException)
            {
                var _message = Database.LOCALIZATIONS["MSG_Item_Does_Not_Exist"];
                _message = _message.Replace("#boss#", FontColorChatSystem.Yellow(BossName));
                throw ctx.Error(_message);
            }
            catch (Exception e)
            {
                throw ctx.Error($"Error: {e.Message}");
            }

            
        }
    }
}
