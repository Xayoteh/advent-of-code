TARGET = main.exe

PUZZLE_PATH = puzzles
DAYS = 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25
DAY_NAMES = $(addprefix \day, $(DAYS))
PUZZLE_NAMES = $(join $(DAYS), $(DAY_NAMES))
PUZZLE_PATHS = $(addprefix $(PUZZLE_PATH)\, $(PUZZLE_NAMES))
PUZZLE_OBJS = $(addsuffix .o, $(PUZZLE_PATHS))

TOOL_PATH = tools
TOOL_NAMES = utilities directions point
TOOL_PATHS = $(addprefix $(TOOL_PATH)\, $(TOOL_NAMES))
TOOL_OBJS = $(addsuffix .o, $(TOOL_PATHS))

OBJS = main.o $(PUZZLE_OBJS) $(TOOL_OBJS)

REBUILDABLES = $(OBJS) $(TARGET)

all: $(TARGET)

$(TARGET) : $(OBJS)
	g++ -g -o $@  $^ -lcrypto

%.o: %.cpp %.hpp
	g++ -o $@ -c $<

clean:
	del $(REBUILDABLES)
#	rf $(REBUILDABLES)