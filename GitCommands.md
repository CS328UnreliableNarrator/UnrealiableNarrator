## List of useful git commands to help workflow with Git and GitHub

1. git clone <repositoryURL>
    - This clones the git repo and is what enables you to initially get all the prject files

2. git pull --rebase 
    - Use this at the start of whenever you are about to add more to the project to ensure that you have the latest changes in your repository to help prevent merge conflicts

3. git add .
    - The . will add all changed files to staging, you can also do individual files by specifying their name instead of using . 

3. git commit -m "commit message here"
    - Easiest way to create a commit with a message, can do without -m flag but will be taken to an editor that either is native to Git or the app you are using and can be tricky to navigate

4. git push 
    - command is run after git commit and pushes your commit to the GitHub repository, this is needed in order for others to pull your changes

5. git push --force 
    - If you are dealing with a merge conflict and have resolved it, this type of push may be needed to have your push actually work, just be careful with this becuase if the conflict isn't properly resolved you can brick the codebase for everyone that pulls your changes. 

Example commit message: v2022.10.08 - Added GitCommands file to the repo 