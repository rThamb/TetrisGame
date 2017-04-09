using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame;

namespace MonoTetrisGame
{
    class ScoreSprite : DrawableGameComponent
    {
        private Score score;
        private Game game;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private Boolean isGameOver;

        public ScoreSprite(Game game, Score score):base(game)
        {
            this.game = game;
            this.score = score;
            isGameOver = false;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = game.Content.Load<SpriteFont>("scoreFont");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if(isGameOver)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "GAME OVER", new Vector2(220, 220), Color.GhostWhite);
                spriteBatch.End();
            }
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Score: " + score.Points, new Vector2(220,130), Color.White);
            spriteBatch.DrawString(font, "Level: " + score.Level, new Vector2(220, 150), Color.White);
            spriteBatch.DrawString(font, "Lines Cleared: " + score.Lines, new Vector2(220, 170), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void GameOver()
        {
            isGameOver = true;
        }
        
    }
}
 