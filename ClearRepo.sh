#!/bin/bash
for f in `git ls-files --exclude-standard --others --directory --ignored`
do
    git filter-branch --force --index-filter "git rm --cached --ignore-unmatch '$f'" --prune-empty --tag-name-filter cat -- --all
done
