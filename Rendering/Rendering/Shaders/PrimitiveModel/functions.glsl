
bool IsSelected(int flags){
	return (flags & 1) == 1;
}

bool ShouldDrawRegular(int mask){
	return (mask & 1) != 0;
}

bool ShouldDrawSelected(int mask){
	return (mask & 2) != 0;
}

bool ShouldDrawHighlight(int mask){
	return (mask & 4) != 0;
}
