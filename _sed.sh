#! /usr/bin/env bash
set -uvx
set -e
cd "$(dirname "$0")"
cwd=`pwd`
ts=`date "+%Y.%m%d.%H%M.%S"`
ver=$(echo $ts | sed -e "s/[.]0/./g")

sed -e "s/﴾VERSION﴿/${ver}/g" README.template.md>README.md
cp -pv README.md $name/

current=$(cd $(dirname $0);pwd)
echo $current
name=`echo "$current" | sed -e 's/.*\/\([^\/]*\)$/\1/'`
echo $name

cd $cwd/$name
sed -i -e "s/<Version>.*<\/Version>/<Version>${ver}<\/Version>/g" $name.csproj
cd $cwd/
echo ${ver}>version.txt
