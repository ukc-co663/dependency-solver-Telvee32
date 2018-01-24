#!/usr/bin/env bash
all:
	# resolve dependencies for .NET Core
	-apt-get update -y
	-apt-get install curl apt-transport-https -y

	curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
	mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg

	sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-xenial-prod xenial main" > /etc/apt/sources.list.d/dotnetdev.list'
	-apt-get update -y
	@echo ".NET Core dependencies satisfied!"

	# install .NET Core
	-apt-get install dotnet-sdk-2.1.3 -y

	# build
	@echo "Commencing project build..."
	dotnet restore CO663.DependencySolver
	dotnet build CO663.DependencySolver
	@echo "Compiled!"
	@echo $(shell ls)
	@echo $(shell ls -la)
