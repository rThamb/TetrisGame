using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace MonoTetrisGame
{
    class BoardSprite:DrawableGameComponent
    {
        private IBoard board;
        private Game game;
        private SpriteBatch spriteBatch;
        private Texture2D emptyBlock;
        private Texture2D filledBlock;

        public BoardSprite(Game game,IBoard board):base(game)
        {
            this.game = game;
            this.board = board;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            emptyBlock = game.Content.Load<Texture2D>("EmptyBlock");
            filledBlock = game.Content.Load<Texture2D>("FilledBlock");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Color renuchanRed = Color.IndianRed;
            spriteBatch.Begin();

            for (int i = 0; i < board.GetLength(0);i++)
            {
                for(int j=0;j<board.GetLength(1);j++)
                {
                    if(board[i,j]==Color.Transparent)
                    {
                        spriteBatch.Draw(emptyBlock, new Vector2(j*19,
                            i*19), renuchanRed);
                    }
                    else
                    {
                        spriteBatch.Draw(filledBlock, new Vector2(j*19,
                            i *19), board[i, j]);
                    }
                }
            }
            spriteBatch.End();
                base.Draw(gameTime);
        }


    }
}
