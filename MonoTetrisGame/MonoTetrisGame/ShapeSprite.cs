using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using TetrisGame; 

namespace MonoTetrisGame
{
    public class ShapeSprite : DrawableGameComponent
    {
        private IShape shape;

        //move down fre
        private Score score;
        private int counterMoveDown;

        //keyboard 
        private KeyboardState oldState;
        //private int counterInput;
        private int threshold;

        private int keyBoardCNTR;

        private Game game;
        private SpriteBatch spriteBatch;

        //render 
        //Assuming we need 4 texture2D objects 
        private Texture2D filledBlock;


        private IBoard board;  


        public ShapeSprite(Game game, IBoard board, Score score)
            : base(game)
        {

            this.game = game;
            this.score = score;
            this.board = board;

            this.shape = board.Shape; 


        }

        public override void Initialize()
        {
            oldState = Keyboard.GetState();
            this.threshold = 10;
            this.keyBoardCNTR = 0;  

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            filledBlock = game.Content.Load<Texture2D>("FilledBlock");
            
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            double dropDelay= (11 - score.Level) * 3 ;
            //drop piece 
            checkInput();

            shape = board.Shape;

            if (counterMoveDown >dropDelay)
            {
                shape.MoveDown();
                counterMoveDown = 0;
            }
            else       
                counterMoveDown++;
            

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            for(int index = 0; index < shape.Length; index++)
                spriteBatch.Draw(filledBlock, new Vector2(shape[index].Position.X * 19,
                    shape[index].Position.Y * 19), shape[index].Colour);

            spriteBatch.End();



            base.Draw(gameTime);
        }

        private void checkInput()
        {
            KeyboardState newState = Keyboard.GetState();

            //right

            if (newState.IsKeyDown(Keys.Right))
            {

                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.Right)) // true if the key has not been released 
                {
                    shape.MoveRight();
                    keyBoardCNTR = 0; //reset counter with every new keystroke
                }

                else // key is being held down 
                {
                    keyBoardCNTR++;
                    if (keyBoardCNTR > threshold)
                    {
                        shape.MoveRight();
                        keyBoardCNTR = 0;
                    }
                }

            }

            // left 

            else if (newState.IsKeyDown(Keys.Left))
            {

                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.Left)) // true if the key has not been released 
                {
                    shape.MoveLeft();
                    keyBoardCNTR = 0; //reset counter with every new keystroke
                }

                else // key is being held down 
                {
                    keyBoardCNTR++;
                    if (keyBoardCNTR > threshold)
                    {
                        shape.MoveLeft();
                        keyBoardCNTR = 0;
                    }
                }
            }


            //up 

            else if (newState.IsKeyDown(Keys.Up))
            {

                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.Up)) // true if the key has not been released 
                {
                    shape.Rotate();
                    keyBoardCNTR = 0; //reset counter with every new keystroke
                }

                else // key is being held down 
                {
                    keyBoardCNTR++;
                    if (keyBoardCNTR > threshold)
                    {
                        shape.Rotate();
                        keyBoardCNTR = 0;
                    }
                }

            }


            //Check if the down has been pressed 
            else if (newState.IsKeyDown(Keys.Down))
            {

                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.Down)) // true if the key has not been released 
                {
                    shape.MoveDown();
                    keyBoardCNTR = 0; //reset counter with every new keystroke
                }

                else // key is being held down 
                {
                    keyBoardCNTR++;
                    if (keyBoardCNTR > 2)
                    {
                        shape.MoveDown();
                        keyBoardCNTR = 0;
                    }
                }
            }

            //Check if the down has been pressed 
            else if (newState.IsKeyDown(Keys.Space))
            {

                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.Space)) // true if the key has not been released 
                {
                    shape.Drop();
                    keyBoardCNTR = 0; //reset counter with every new keystroke
                }

            }

            oldState = newState;
        }
    }
}
