# Deployment Guide for HappyBFDay

## Option 1: Railway (Recommended)

### Step 1: Create Railway Account
1. Go to https://railway.app
2. Sign up with GitHub
3. Connect your GitHub account

### Step 2: Deploy from GitHub
1. Push this code to a GitHub repository
2. In Railway dashboard, click "New Project"
3. Select "Deploy from GitHub repo"
4. Choose your repository
5. Railway will automatically detect it's a .NET app and deploy it

### Step 3: Get Your Public URL
1. After deployment, Railway will give you a public URL like: `https://your-app-name.railway.app`
2. Update the QR code URL in HomeController.cs to use this URL

## Option 2: Render

### Step 1: Create Render Account
1. Go to https://render.com
2. Sign up with GitHub

### Step 2: Deploy
1. Push code to GitHub
2. In Render dashboard, click "New +"
3. Select "Web Service"
4. Connect your GitHub repository
5. Use these settings:
   - Build Command: `dotnet publish -c Release -o ./publish`
   - Start Command: `dotnet ./publish/HappyBFDay.dll`

## Option 3: Heroku

### Step 1: Install Heroku CLI
1. Download from https://devcenter.heroku.com/articles/heroku-cli

### Step 2: Deploy
```bash
# Login to Heroku
heroku login

# Create app
heroku create your-app-name

# Deploy
git push heroku main
```

## After Deployment
1. Get your public URL (e.g., https://your-app.railway.app)
2. Update the QR code URL in HomeController.cs:
   ```csharp
   var romanticUrl = "https://your-app.railway.app/Home/RomanticMessage";
   ```
3. Redeploy to update the QR code
