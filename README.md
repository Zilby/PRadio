# PRadio
The p definitely doesn't stand for sketchy content - GGJ 2018

# Setup
- Download Unity 2018 here if you haven't already: https://store.unity.com/download?ref=personal
- If you're on windows and don't have GitBash, download this here: https://git-scm.com/download/win
- Link your computer to your github account via SSH or HTML if you haven't already. You can learn how to do that here: https://help.github.com/articles/adding-a-new-ssh-key-to-your-github-account/#platform-windows (I recommend setting up via SSH since it's a bit easier and is more secure). 
- Once all of that is set up, clone this repository (SSH link: git@github.com:Zilby/PRadio.git, HTML link: https://github.com/Zilby/PRadio.git). You can do this by pasting `git clone _insert_link_here_` into either GitBash (you can paste using `shift + insert`) or the terminal if you're on mac. 

# Git basics
- Adding file changes to git is easy! First make sure your repository/branch is updated by using the `git pull` command. 
- Next, navigate to the `Assets` folder in Unity and do `git add *` (alternatively you can add files individually, `*` will add all file changes). 
- After adding your files, do `git commit -m "_your_message_here_"`. Try to make your commit messages meaningful so we can identify what changes were made. Committing early and often is a good habit!
- Now that all that is out of the way, enter `git push` and your changes will be pushed to the online repository!

# Working with Branches
- Branches assist with version control, they help prevent conflicts when multiple people are working on the same files and make it easier to merge code. Any code changes made on one branch will not affect the main branch (or other branches) until the branch is merged into main. 
- To make a new branch enter `git checkout -b _new_branch_title_`. This command creates your new branch locally. To push your new branch to the remote server enter `git push -u origin _new_branch_title_`
- The default branch is `master`, this branch is the main branch for the project and should more or less not be modified directly (outside of smaller edits). 
- To switch between branches, use the `git checkout` command. Eg: `git checkout master` will change your branch to master. `git checkout _your_branch_title_` will switch your current local branch to yours. You will need to do this after making a new branch as well. 

# Merging in your branch
- To update your branch to the most recent changes from master, first make sure you're on your branch and that it's updated (use `git pull`), and then use `git merge origin/master`. This may create conflicts! If you need help resolving conflicts, come to Alex. 
- Note: a common mistake is to do `git merge master` (not `origin/master`) without using `git pull` on your master branch, all this does is merge in your local version of master, which most likely does not have the most recent changes. 
- Once updated, to merge in your branch to master, the best way is to create a pull request (you can use this link here: https://github.com/Zilby/PRadio/compare). Select your branch under compare and fix any conflicts that appear. If all else checks out, feel free to merge in the pull request, and your changes are now in the final game!
