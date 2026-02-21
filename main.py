import discord
from discord.ext import commands
import subprocess
import os

bot = commands.Bot(command_prefix=".", intents=discord.Intents.all())

@bot.command()
async def deobf(ctx):
    if not ctx.message.attachments:
        return await ctx.send("attach lua file son!")

    attachment = ctx.message.attachments[0]
    lua_code = (await attachment.read()).decode("utf-8")
    
    # Geçici dosyalar oluştur
    with open("input.lua", "w") as f: f.write(lua_code)
    
    secret = os.environ.get('AUTH_KEY')
    await ctx.send("⚙️ **Moonsec V3 working** okay ...")

    try:
        # C# Projesini tetikle (Projende .csproj adını kontrol et!)
        res = subprocess.run(
            ["dotnet", "run", "--project", "Moonsec.csproj", secret, "-dis", "-i", "input.lua", "-o", "output.lua"],
            capture_output=True, text=True
        )

        if os.path.exists("output.lua"):
            await ctx.send("✅ done son!", file=discord.File("output.lua"))
        else:
            await ctx.send(f"❌ error i cant find baby in island: {res.stdout}")

    except Exception as e:
        await ctx.send(f"❌ Teknik Hata: {e}")

bot.run(os.environ.get('DISCORD_TOKEN'))
