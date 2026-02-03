using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoTemplate;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private const int _screenWidth = 750, _screenHeight = 450;
    private const int _playAreaEdgeLineWidth = 12, _ballWidthAndHeight = 21;
    private float _ballSpeed;
    private Vector2 _ballPosition, _ballDirection;
    private Texture2D _backgroundTexture, _ballTexture;
    private Rectangle _playAreaBoundingBox;
    private Rectangle _ballRectangle;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferWidth = _screenWidth;
        _graphics.PreferredBackBufferHeight = _screenHeight;

        _ballPosition = new Vector2(_screenWidth / 2, _screenHeight / 2);
        _ballSpeed = 100;

        _ballDirection.X = 1;
        _ballDirection.Y = 1;

        _playAreaBoundingBox = new Rectangle(0, 0, _screenWidth, _screenHeight);

        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Load the textures
        _backgroundTexture = Content.Load<Texture2D>("Court");
        _ballTexture = Content.Load<Texture2D>("Ball");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Collision Detection
        if (_ballRectangle.Left <= _playAreaBoundingBox.Left ||
            _ballRectangle.Right >= _playAreaBoundingBox.Right)
        {
            _ballDirection.X *= -1;
        }
        if (_ballRectangle.Bottom >= _playAreaBoundingBox.Bottom ||
            _ballRectangle.Top <= _playAreaBoundingBox.Top)
        {
            _ballDirection.Y *= -1;
        }
        _ballPosition += _ballDirection * _ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        _ballRectangle = new Rectangle((int)_ballPosition.X, (int)_ballPosition.Y, _ballWidthAndHeight, _ballWidthAndHeight);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Draw the game space
        _spriteBatch.Begin();
        _spriteBatch.Draw(_backgroundTexture, _playAreaBoundingBox, Color.Green);


        _spriteBatch.Draw(_ballTexture, _ballRectangle, Color.White);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
